using ProyectoDeGraduacion.BaseDatos;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;


namespace ProyectoDeGraduacion.Models
{
    public class FiltroSeguridad : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Si la acción o el controlador tienen AllowAnonymous, omite la validación
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (filterContext.HttpContext.Session["NombreUsuario"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller", "Login" },
                { "action", "Login" }
            });
            }

            base.OnActionExecuting(filterContext);
        }
    }


    public class FiltroAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["RolUsuario"] == null ||
                filterContext.HttpContext.Session["RolUsuario"].ToString() != "1")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller", "Login" },
                { "action", "Login" }
            });
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class ValidarSesionUnicaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["idUsuario"] != null)
            {
                int idUsuario = (int)filterContext.HttpContext.Session["idUsuario"];
                string sessionToken = filterContext.HttpContext.Session["SessionToken"] as string;

                using (var context = new ProyectoGraduacionEntities())
                {
                    var user = context.tPacientes.FirstOrDefault(u => u.idPaciente == idUsuario);
                    // Si el token registrado en la base de datos no coincide con el de la sesión,
                    // significa que se inició una sesión en otro dispositivo
                    if (user != null && user.SessionToken != sessionToken)
                    {
                        filterContext.HttpContext.Session.Clear();
                        filterContext.Result = new RedirectResult("~/Login/Login?mensaje=La sesión ha sido finalizada porque se inició una sesión en otro dispositivo.");
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
