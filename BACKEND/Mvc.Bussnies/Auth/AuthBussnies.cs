using DtoModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.Auth;

public class AuthBussnies : IAuthBussnies
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public Task<bool> IsCredentialsOk(LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "123456")
        {
            return Task.FromResult(true);
        }
        else
        {
            return Task.FromResult(false);
        }
    }

    public Task<LoginResponse?> Login(LoginRequest request)
    {

        if (request.Username == "admin" && request.Password == "123456")
        {
            return Task.FromResult(new LoginResponse
            {
                Expiration = DateTime.UtcNow.AddHours(1),
                Token = "fake-jwt-token",
                UserId = "1"
            })!;
        }
        else
        {
            return null;
        }

    }
}