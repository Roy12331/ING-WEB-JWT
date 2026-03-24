using DtoModel.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mvc.Bussnies.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthBussnies _authBussnies;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthBussnies authBussnies, IConfiguration configuration)
        {
            _authBussnies = authBussnies;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("El servicio está escuchando");
        }


        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {

            bool isLogged = await _authBussnies.IsCredentialsOk(request);

            if (!isLogged) { return BadRequest(); }

            LoginResponse result = new LoginResponse
            {
                UserId = request.Username, // Aquí puedes usar el ID real del usuario
                Expiration = DateTime.UtcNow.AddHours(1), // El token expirará en 1 hora
                Token = GenerateToken() // Genera un token JWT
            };



            return Ok(result);
        }


        private string GenerateToken()
        {
            // Obtener la configuración de JWT desde appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");

            // Crear la clave de seguridad
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Definir los claims (datos del usuario que se incluirán en el token)
            var claims = new[]
            {
                new Claim("username", "fhuaman"),
                new Claim("rolId", "1"),
                new Claim("roleName", "Administrador"),
                new Claim("fullName", "Franklin Huaman"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            // Retornar el token como string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
