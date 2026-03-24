using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModel.Login
{
    public class LoginResponse
    {
        public DateTime Expiration { get; set; }
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

    }
    //que la información de correo, nombre, celular, etc, se construya en un nuevo end point
    //userController => getUserInfo => userId => return userInfo
}
