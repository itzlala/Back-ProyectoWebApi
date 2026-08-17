using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Data;
using WebApi.Models;
using WebApi.Security;

namespace WebApi.Controllers
{
    [InventoryAuthorize]
    public class CuentaController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<AccountSummary> Get()
        {
            return CuentaData.ListarCue().Select(cuenta => new AccountSummary
            {
                IdCuenta = cuenta.IdCuenta,
                Usuario = cuenta.Usuario,
                FechaRegistro = cuenta.FechaRegistro
            });
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var cuenta = CuentaData.ObtenerCue(id);
            if (cuenta == null || cuenta.IdCuenta == 0) return NotFound();

            return Ok(new AccountSummary
            {
                IdCuenta = cuenta.IdCuenta,
                Usuario = cuenta.Usuario,
                FechaRegistro = cuenta.FechaRegistro
            });
        }

        // POST api/<controller>
        public bool Post([FromBody] Cuentas oUsuario)
        {
            return CuentaData.RegistrarCue(oUsuario);
        }

        // PUT api/<controller>/5
        public bool Put([FromBody] Cuentas oUsuario)
        {
            return CuentaData.ModificarCue(oUsuario);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return CuentaData.EliminarCue(id);
        }
    }
}
