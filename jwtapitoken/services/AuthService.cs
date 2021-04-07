using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jwtapitoken.Helpers;
using jwtapitoken.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace jwtapitoken.services
{
    public class AuthService:IAuthService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }


        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            // handling Email & Username registration (if email/username already in database show error message)
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel {Message = "Email already register"};


            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username already register" };

            // mapping model 
            var User = new AppUser()
            {
                UserName = model.Username ,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
                

            };

            // register User that we create VAR User
            // hashing Password

          var Result =   await _userManager.CreateAsync(User, model.Password);


          // errors from registration 


          if (!Result.Succeeded)
          {
              var errors = string.Empty;
              foreach (var er in Result.Errors)
              {
                  errors += $"{er.Description},"+ Environment.NewLine ;


              }
              return new AuthModel { Message = errors };

          }

          // assign user from registration to role student
          await _userManager.AddToRoleAsync(User, "Student");


          var JwtSecurityToken = await CreateJwtToken(User);




          // return data 

          return new AuthModel
          {
              Email = User.Email,
              ExpiresOn = JwtSecurityToken.ValidTo,
              IsAuthenticated = true,
              Roles = new List<string> { "User"} ,
              Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken) ,
              UserName = User.UserName

          };


        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }






        // generate JWT Token

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


    }
}
