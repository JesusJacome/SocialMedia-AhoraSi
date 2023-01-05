using Microsoft.Extensions.Options;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using System.Security.Cryptography;

namespace SocialMedia.Infrastructure.Services
{
    public class PasswordService : IPasswordHasher
    {
        private readonly PasswordOptions options;

        public PasswordService(IOptions<PasswordOptions> options)
        {
            this.options = options.Value;
        }
        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes
                (
                    password,
                    options.SaltSize,
                    options.Iterations
                ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{options.Iterations}.{salt}.{key}";
            }
        }

        public bool Check(string hash, string password)
        {
            throw new NotImplementedException();
        }

    }
}
