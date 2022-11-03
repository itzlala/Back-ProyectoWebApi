using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Inventario
    {
        public int IdInventario { get; set; }
        public string Folio { get; set; }
        public string Tipo { get; set; }
        public string Estatus { get; set; }
        public string DescFis { get; set; }
        public string DescTec { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NomProd { get; set; }
        public string Nserie { get; set; }
        public string Costo { get; set; }
        public string Lugar { get; set; }
        public string Asignacion { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}