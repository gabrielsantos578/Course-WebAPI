using Jose;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseGuide.Objects.Contracts
{
    public class Token
    {
        [Required(ErrorMessage = "O token é requerido!")]
        public string TokenAccess { get; set; }

        public void GenerateToken(string email)
        {
            TokenSignatures securityEntity = new();

            var payload = new Dictionary<string, object>
            {
                { "iss", securityEntity.Issuer },
                { "aud", securityEntity.Audience },
                { "sub", email },
                { "exp", DateTimeOffset.UtcNow.AddMinutes(60).ToUnixTimeSeconds() }
            };

            this.TokenAccess = JWT.Encode(payload, Encoding.UTF8.GetBytes(securityEntity.Key), JwsAlgorithm.HS256);
        }

        public bool ValidateToken()
        {
            TokenSignatures securityEntity = new();

            // Passo 1: Verificar se o token tem três partes
            string[] tokenParts = this.TokenAccess.Split('.');
            if (tokenParts.Length != 3)
            {
                return false;
            }

            try
            {
                // Passo 2: Decodificar o token
                string decodedToken = Encoding.UTF8.GetString(Base64Url.Decode(tokenParts[1]));

                // Passo 3: Verificar issue e audience
                var payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(decodedToken);
                if (!payload.TryGetValue("iss", out object issuerClaim) ||
                    !payload.TryGetValue("aud", out object audienceClaim))
                {
                    return false;
                }

                if (issuerClaim.ToString() != securityEntity.Issuer || audienceClaim.ToString() != securityEntity.Audience)
                {
                    return false;
                }

                // Passo 4: Verificar se o tempo expirou
                if (payload.TryGetValue("exp", out object expirationClaim))
                {
                    long expirationTime = long.Parse(expirationClaim.ToString());
                    var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationTime).UtcDateTime;
                    if (expirationDateTime < DateTime.UtcNow)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ExtractSubject()
        {
            // Verificar se o token tem três partes
            string[] tokenParts = this.TokenAccess.Split('.');
            if (tokenParts.Length != 3)
            {
                return string.Empty;
            }

            try
            {
                // Decodificar a parte do payload do token
                string decodedToken = Encoding.UTF8.GetString(Base64Url.Decode(tokenParts[1]));

                // Deserializar o payload para um dicionário
                var payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(decodedToken);

                // Extrair o subject (email)
                if (payload.TryGetValue("sub", out object subjectClaim))
                {
                    return subjectClaim.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}