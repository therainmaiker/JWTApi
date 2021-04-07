using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtapitoken.Models;

namespace jwtapitoken.services
{
    public interface IAuthService
    {


        Task<AuthModel> RegisterAsync(RegisterModel model);
         Task<AuthModel> GetTokenAsync(TokenRequestModel model);

    }
}
