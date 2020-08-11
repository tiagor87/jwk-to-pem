using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwkToPem.Core
{
    public static class JsonWebTokenExtensions
    {
        public static bool VerifySignature(this JsonWebKey keySet, string token)
        {
            var tokenParts = token.Split('.');

            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(
                new RSAParameters() {
                    Modulus = FromBase64Url(keySet.N),
                    Exponent = FromBase64Url(keySet.E)
                });

            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenParts[0] + '.' + tokenParts[1]));

            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA256");
            
            return rsaDeformatter.VerifySignature(hash, FromBase64Url(tokenParts[2]));
        }
        
        private static byte[] FromBase64Url(string base64Url)
        {
            var padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            var base64 = padded.Replace("_", "/")
                .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
    }
}
