using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDeGraduacion.Models
{
    public class ChatbotModel
    {
        private readonly string apiKey;

        public ChatbotModel()
        {
            apiKey = System.Configuration.ConfigurationManager.AppSettings["OpenAIApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("API Key no encontrada en el archivo web.config.");
            }
        }

        public async Task<string> ObtenerRespuestaAsync(string pregunta)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                        new { role = "system", content = "Eres un asistente virtual exclusivo para una Clínica Dental Dra. Mariana Garro. Cuando te pregunten sobre los siguientes temas recuerda siempre mencionar el nombre de la clinica: horarios responde que son de lunes a viernes de 7:00 am a 5:00 pm y sabados de 7:00 am a 2:00 pm, " +
                        "y cuando te pregunten sobre servicios di que se hacen los siguientes, Calzas, extracciones, blanquemientos, Ortodoncia, endodoncia, implantes, coronas, puentes y rellenos dentales" },
                        new { role = "user", content = pregunta }
                    },
                    max_tokens = 100 
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error de la API de OpenAI: {response.StatusCode} - {errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseContent);

                return result.choices[0].message.content.ToString();
            }
        }
    }
}



