using Logeecom.DemoProjekat.Exceptions;
using Logeecom.DemoProjekat.DAL.Models;
using Logeecom.DemoProjekat.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Logeecom.DemoProjekat.BL.Services
{
    public class LoginService
    {
        private readonly UserRepository userRepository;

        public LoginService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void CreateAdmin(User admin)
        {
            admin.Password = HashPassword(admin.Password);
            this.userRepository.CreateAdmin(admin);
        }

        public string Login(User user, string secretKey, string issuer, string audience)
        {
            var admin = this.userRepository.FindUser(user.Username);
            if (admin == null || !passwordMatches(user.Password, admin.Password))
            {
                throw new UnauthenticatedException("Unauthorized access.");
            }

            return this.GenerateJSONWebToken(secretKey, issuer, audience);
        }

        private bool passwordMatches(string password, string adminPassword)
        {
            return this.HashPassword(password) == adminPassword;
        }

        private string GenerateJSONWebToken(string secretKey, string issuer, string audience)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer,
              audience,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder hashedPassword = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    hashedPassword.Append(bytes[i].ToString("x2"));
                }

                return hashedPassword.ToString();
            }
        }
    }
}
