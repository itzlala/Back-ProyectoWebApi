using System.Collections.Generic;
using System.Web.Http;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class InventarioController : ApiController
    {
        // GET: Inventario
        // GET api/<controller>
        public List<Inventario> Get()
        {
            return InventarioData.Listar();
        }
        // GET api/<controller>/5
        public Inventario Get(int id)
        {
            return InventarioData.Obtener(id);
        }

        // POST api/<controller>
        public bool Post([FromBody] Inventario oInventario)
        {
            return InventarioData.Registrar(oInventario);
        }

        // PUT api/<controller>/5
        public bool Put([FromBody] Inventario oInventario)
        {
            return InventarioData.Modificar(oInventario);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return InventarioData.Eliminar(id);
        }
    }
}