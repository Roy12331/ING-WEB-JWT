using DtoModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.Auth
{
    public interface IAuthBussnies : IDisposable
    { 
        Task<LoginResponse?> Login(LoginRequest request);

        Task<bool> IsCredentialsOk(LoginRequest request);
    }
}
