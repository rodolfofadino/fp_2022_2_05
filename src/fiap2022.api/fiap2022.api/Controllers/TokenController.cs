using fiap2022.api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fiap2022.api.Controllers
{
    [Route("/token")]
    [ApiController]
    public class TokenController : Controller
    {
        [HttpPost]
        public IActionResult Create(TokenInfo model)
        {
            if (IsValidUserAndPassword(model.UserName, model.Password))
            {
                var token = GenerateToken(model.UserName);

                return new OkObjectResult(new { token = token });
            }


            return new BadRequestResult();
        }

        private string GenerateToken(string userName)
        {
            var claims = new Claim[]
               {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
               };

            var symmetricSecurityKey = new SymmetricSecurityKey(Security.GetKey());
            var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var jwtHeader = new JwtHeader(signingCredentials);
            var jwtPayload = new JwtPayload(claims);

            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsValidUserAndPassword(string userName, string password)
        {
            return userName == "rafael" && password == "r4f43l!";
        }
    }
}
