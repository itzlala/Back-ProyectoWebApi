using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace WebApi.Security
{
    public static class JwtTokenService
    {
        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer();

        public static string Crear(string usuario, string rol, DateTime expiraEn)
        {
            var header = new Dictionary<string, object>
            {
                { "alg", "HS256" },
                { "typ", "JWT" }
            };
            var payload = new Dictionary<string, object>
            {
                { "sub", usuario },
                { "name", usuario },
                { "role", rol },
                { "iss", Configuracion("JwtIssuer") },
                { "aud", Configuracion("JwtAudience") },
                { "iat", Epoch(DateTime.UtcNow) },
                { "exp", Epoch(expiraEn) }
            };

            var contenido = Base64Url(Json.Serialize(header)) + "." + Base64Url(Json.Serialize(payload));
            return contenido + "." + Firmar(contenido);
        }

        public static bool Validar(string token, out string usuario, out string rol)
        {
            usuario = null;
            rol = null;
            var partes = (token ?? string.Empty).Split('.');
            if (partes.Length != 3 || !ComparacionSegura(partes[2], Firmar(partes[0] + "." + partes[1])))
            {
                return false;
            }

            try
            {
                var payload = Json.Deserialize<Dictionary<string, object>>(Decodificar(partes[1]));
                if (Convert.ToInt64(payload["exp"]) <= Epoch(DateTime.UtcNow) ||
                    !string.Equals(Convert.ToString(payload["iss"]), Configuracion("JwtIssuer"), StringComparison.Ordinal) ||
                    !string.Equals(Convert.ToString(payload["aud"]), Configuracion("JwtAudience"), StringComparison.Ordinal))
                {
                    return false;
                }

                usuario = Convert.ToString(payload["sub"]);
                rol = Convert.ToString(payload["role"]);
                return !string.IsNullOrWhiteSpace(usuario);
            }
            catch
            {
                return false;
            }
        }

        private static string Firmar(string contenido)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Configuracion("JwtSecret"))))
            {
                return Base64Url(hmac.ComputeHash(Encoding.UTF8.GetBytes(contenido)));
            }
        }

        private static string Configuracion(string clave)
        {
            var environmentKey = clave == "JwtSecret" ? "NEXO_JWT_SECRET" : "NEXO_" + clave.ToUpperInvariant();
            var valor = Environment.GetEnvironmentVariable(environmentKey) ?? ConfigurationManager.AppSettings[clave];
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ConfigurationErrorsException("Falta la configuracion '" + clave + "'.");
            }
            if (clave == "JwtSecret" && (valor.Length < 32 || valor.StartsWith("CAMBIAR-", StringComparison.Ordinal)))
            {
                throw new ConfigurationErrorsException("Configure NEXO_JWT_SECRET con una clave privada de al menos 32 caracteres.");
            }
            return valor;
        }

        private static long Epoch(DateTime fecha)
        {
            return Convert.ToInt64((fecha.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
        }

        private static string Base64Url(string valor)
        {
            return Base64Url(Encoding.UTF8.GetBytes(valor));
        }

        private static string Base64Url(byte[] valor)
        {
            return Convert.ToBase64String(valor).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        private static string Decodificar(string valor)
        {
            var base64 = valor.Replace('-', '+').Replace('_', '/');
            base64 = base64.PadRight(base64.Length + ((4 - base64.Length % 4) % 4), '=');
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }

        private static bool ComparacionSegura(string izquierda, string derecha)
        {
            var a = Encoding.UTF8.GetBytes(izquierda ?? string.Empty);
            var b = Encoding.UTF8.GetBytes(derecha ?? string.Empty);
            if (a.Length != b.Length) return false;
            var diferencia = 0;
            for (var i = 0; i < a.Length; i++) diferencia |= a[i] ^ b[i];
            return diferencia == 0;
        }
    }
}
