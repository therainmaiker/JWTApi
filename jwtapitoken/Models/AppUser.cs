using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace jwtapitoken.Models
{
    public class AppUser:IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ResCounter { get; set; } = 0;

    }
}
