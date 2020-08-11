using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace JwkToPem.Core.Tests
{
    public class JsonWebTokenExtensionsTests
    {
        [Fact]
        public void GivenKeyWhenVerifyTokenShouldReturnTrue()
        {
            var key = new JsonWebKey(@"
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

            var token =
                "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkJEYXo0bE5jZ0Y2eHRsY0RfVDl6bzFUU1BaRSJ9.eyJhdWQiOiJKUE1DOlVSSTpSUy0xMDYzNTktNTI5MDYtTVNHYXRld2F5TXVuZGlwYWdnLVVBVCIsImlzcyI6Imh0dHA6Ly9pZGF1YXQuanBtb3JnYW5jaGFzZS5jb20vYWRmcy9zZXJ2aWNlcy90cnVzdCIsImlhdCI6MTU5NzE1MjYwMCwiZXhwIjoxNTk3MTgxNDAwLCJKUE1DSWRlbnRpZmllciI6Ik43MzcyOTQiLCJSb2xlIjpbIk11bmRpcGFnZ19TdWJzY3JpcHRpb25zX1NhbmRib3gtNTI5MDYtMTA2MzU5LVVBVCIsIk11bmRpcGFnZ19JbnZvaWNlc19TYW5kYm94LTUyOTA2LTEwNjM1OS1VQVQiLCJNdW5kaXBhZ2dfUGxhbnNfU2FuZGJveC01MjkwNi0xMDYzNTktVUFUIiwiTXVuZGlwYWdnX0NoYXJnZXNfU2FuZGJveC01MjkwNi0xMDYzNTktVUFUIiwiTXVuZGlwYWdnX09yZGVyc19TYW5kYm94LTUyOTA2LTEwNjM1OS1VQVQiXSwiQ2xpZW50SVBBZGRyZXNzIjoiMTY5LjkyLjAuMjAiLCJhcHB0eXBlIjoiQ29uZmlkZW50aWFsIiwiYXBwaWQiOiJDQy0xMDgwNDgtTjczNzI5NC03MDQxMC1VQVQiLCJhdXRobWV0aG9kIjoiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2F1dGhlbnRpY2F0aW9ubWV0aG9kL3dpbmRvd3MiLCJhdXRoX3RpbWUiOiIyMDIwLTA4LTExVDEzOjMwOjAwLjUyMVoiLCJ2ZXIiOiIxLjAifQ.f4_3gzsOkv9xYoHtINKtWkzTtPD7C43iROiy7fKKEDbC1vduML2zHXPVUTWjumrT2SjfU34JdT2RaCycgTe867Dwa4mBc5V6d6mihOtZRFJ2iDKlGum2feSKOrf_GRoSkDZwIQcC6ZdrqsdfxZkNuNCyHkKLTLC5h8BJ51J5m1ftnjek-J7k1a_oefyjaq2NUp5WOQn6b6HUwBZCY_Y79j-CaR1EkuKmeac-8hjlV0LJ4SUt2asDqMHhgkJ7MCkon-4xivB_I5mVgdgLuH8RMJEBV0uQQ_S43LT8ArDFZRrJQRIAM1KLf4mDBFdYvT_4YSdqPNyPFUagbfvRDQfVbg";

            key.VerifySignature(token).Should().BeTrue();
        }
    }
}
