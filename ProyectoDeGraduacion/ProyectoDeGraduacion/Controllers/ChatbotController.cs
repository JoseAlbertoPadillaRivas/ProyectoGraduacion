using ProyectoDeGraduacion.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
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
            // Obtener la respuesta desde el modelo del chatbot
            var respuesta = await chatbotModel.ObtenerRespuestaAsync(pregunta);

            // Guardar la respuesta en TempData para que esté disponible tras la redirección
            TempData["Respuesta"] = respuesta;

            // Redirigir a la acción Index del controlador Login
            return RedirectToAction("Index", "Login");
        }




    }
}
