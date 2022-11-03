using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Cuentas
    {
        public int IdCuenta { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}