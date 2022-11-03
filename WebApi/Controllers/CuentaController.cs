using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CuentaController : ApiController
    {
        // GET api/<controller>
        public List<Cuentas> Get()
        {
            return CuentaData.ListarCue();
        }

        // GET api/<controller>/5
        public Cuentas Get(int id)
        {
            return CuentaData.ObtenerCue(id);
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
