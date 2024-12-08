using ProyectoDeGraduacion.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class ChatbotController : Controller
    {
        private readonly ChatbotModel chatbotModel;

        public ChatbotController()
        {
            chatbotModel = new ChatbotModel();
        }
        [HttpPost]
        public async Task<ActionResult> ObtenerRespuesta(string pregunta)
        {
            var respuesta = await chatbotModel.ObtenerRespuestaAsync(pregunta);

            TempData["Respuesta"] = respuesta;

            return RedirectToAction("Index", "Login");
        }
    }
}
