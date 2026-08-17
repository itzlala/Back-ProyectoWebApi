using System;

namespace WebApi.Models
{
    public class AccountSummary
    {
        public int IdCuenta { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
