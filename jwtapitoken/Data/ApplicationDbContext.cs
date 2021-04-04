using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtapitoken.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwtapitoken.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) :base(options)
        {
            
        }


    }
}
