using System;
using System.Net;
using System.Web.Http;
using WebApi.Data;
using WebApi.Models;
using WebApi.Security;

namespace WebApi.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] AuthRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest("Usuario y contrasena son obligatorios.");
            }

            var cuenta = CuentaData.Autenticar(request.Usuario, request.Contrasenia);
            if (cuenta == null)
            {
                return Content(HttpStatusCode.Unauthorized, new
                {
                    mensaje = "Las credenciales no son validas."
                });
            }

            var expiracion = DateTime.UtcNow.AddHours(8);
            return Ok(new AuthResponse
            {
                Token = JwtTokenService.Crear(cuenta.Usuario, "Administrador", expiracion),
                Usuario = cuenta.Usuario,
                Rol = "Administrador",
                ExpiraEn = expiracion
            });
        }
    }
}
