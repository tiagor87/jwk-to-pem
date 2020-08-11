using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace JwkToPem.Core
{
    public class JpKeys : IEnumerable<JsonWebKey>
    {
        private string _audience => "JPMC:URI:RS-106359-52906-MSGatewayMundipagg-UAT";
        private string _issuer => "http://idauat.jpmorganchase.com/adfs/services/trust";

        private TokenValidationParameters ValidationParameters => new TokenValidationParameters
        {
            RequireExpirationTime = false,
            RequireSignedTokens = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = _audience,
            ValidIssuer = _issuer,
            IssuerSigningKeys = this
        };

        public SecurityToken GetValidatedToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, ValidationParameters, out var validatedToken);
            return validatedToken;
        }
        
        public IEnumerator<JsonWebKey> GetEnumerator()
        {
            yield return new JsonWebKey(@"
            {
                ""kty"": ""RSA"",
                ""use"": ""sig"",
                ""alg"": ""RS256"",
                ""kid"": ""BDaz4lNcgF6xtlcD_T9zo1TSPZE"",
                ""x5t"": ""BDaz4lNcgF6xtlcD_T9zo1TSPZE"",
                ""n"": ""tf9UnhB9rcTqXu2pFW4xI2iWg5DzEi8acxJd9SmWVEXqOxH791dg7dl4TiBKCu3ghFS4Nx4xgOi1X9jsCrOpDStpqD1LklIPBQ0N4elzprttO82JgMfZLzOyTdFvoVc0hRq7ChKffvueis98UNxTZ1Rt3skSM2g8xBiM6GTss6CCETPcmGztgG-An7cqnGZ3m3hxkh27FO-uay21CruoJul0YZMj3AUJk6iMovesyfnRR1_8Kr36MwYitImGg5GjF1JeS187xR7cNjHknwSECyIK6GBZi9br1RDKxPqOR6NkzuWMrF0IdjO7zu9cLUEtxuVbvRBsLYHlZJNwOc7gsQ"",
                ""e"": ""AQAB"",
                ""x5c"": [
                    ""MIIC7DCCAdSgAwIBAgIQPiUp6wRuLaBM5MTNqvglozANBgkqhkiG9w0BAQsFADAyMTAwLgYDVQQDEydBREZTIFNpZ25pbmcgLSBpZGF1YXQuanBtb3JnYW5jaGFzZS5jb20wHhcNMTkxMDA4MTg0NDM0WhcNMjkxMDA1MTg0NDM0WjAyMTAwLgYDVQQDEydBREZTIFNpZ25pbmcgLSBpZGF1YXQuanBtb3JnYW5jaGFzZS5jb20wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC1\/1SeEH2txOpe7akVbjEjaJaDkPMSLxpzEl31KZZUReo7Efv3V2Dt2XhOIEoK7eCEVLg3HjGA6LVf2OwKs6kNK2moPUuSUg8FDQ3h6XOmu207zYmAx9kvM7JN0W+hVzSFGrsKEp9++56Kz3xQ3FNnVG3eyRIzaDzEGIzoZOyzoIIRM9yYbO2Ab4CftyqcZnebeHGSHbsU765rLbUKu6gm6XRhkyPcBQmTqIyi96zJ+dFHX\/wqvfozBiK0iYaDkaMXUl5LXzvFHtw2MeSfBIQLIgroYFmL1uvVEMrE+o5Ho2TO5YysXQh2M7vO71wtQS3G5Vu9EGwtgeVkk3A5zuCxAgMBAAEwDQYJKoZIhvcNAQELBQADggEBAJxaXST0mI62TsNpXb2u+nhtj2o+Okj98Yx6mpGtgwIrmXK0xJxuNp9tVtm\/6RCHtQ\/RWMcbGKXHhLGO+aZXi1S2m7vFb\/NPiu1cZVDxqUj4fLGMyof3Jsy\/CC\/9TZYK1HQW7+Nq36fhZwsYbG\/dEU3O2htgX8fvolxoxd7Wp2zqDZJ9u4eiIdgYXavz3Gm8voM8\/TXHLJC086JDHN7Z8Yke0zHOSxVTY+UloviMOhRFAvWzbfLWSyFYERNjHM3oPMvLrtXMfPno9kYVrVSKyx3UsnrucWHkishPWoH65vPzubSz8t6Zf03MmShxFDfXN68jrHWfxV\/5Rbk+rKKI0lI=""
                ]
            }");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
