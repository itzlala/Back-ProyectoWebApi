using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Security
{
    public class InventoryAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authorization = actionContext.Request.Headers.Authorization;
            string usuario;
            string rol;

            if (authorization == null || authorization.Scheme != "Bearer" ||
                !JwtTokenService.Validar(authorization.Parameter, out usuario, out rol))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    mensaje = "Se requiere una sesion valida."
                });
                return;
            }

            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario),
                new Claim(ClaimTypes.Role, rol)
            }, "Bearer");
            var principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;
            actionContext.RequestContext.Principal = principal;
        }
    }
}
