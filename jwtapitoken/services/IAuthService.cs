using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtapitoken.Models;

namespace jwtapitoken.services
{
    interface IAuthService
    {


        Task<AuthModel> RegisterAsync(RegisterModel model);


    }
}
