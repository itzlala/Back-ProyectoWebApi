using System;

namespace WebApi.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public DateTime ExpiraEn { get; set; }
    }
}
