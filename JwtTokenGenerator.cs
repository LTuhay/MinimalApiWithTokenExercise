
using Microsoft.IdentityModel.Tokens;
using MinimalApiWithToken.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinimalApiWithToken
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserDB _context;

        public JwtTokenGenerator(IConfiguration configuration, UserDB context) 
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _context = context ?? throw new ArgumentNullException( nameof(context));
        }
        public string? GenerateToken(string userName, string password)
        {
            User? user = ValidateUserCredentials(userName, password);
            if (user == null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(
                 Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.UserName));

            var jwtSecurityToken = new JwtSecurityToken(
                 _configuration["Authentication:Issuer"],
                 _configuration["Authentication:Audience"],
                 claimsForToken,
                 DateTime.UtcNow,
                 DateTime.UtcNow.AddHours(1),
                 signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return tokenToReturn;

        }


        public User? ValidateUserCredentials(string userName, string password) 
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
            return user;

        }


    }
}
