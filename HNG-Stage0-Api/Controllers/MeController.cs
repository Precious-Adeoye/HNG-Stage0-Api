using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HNG_Stage0_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://catfact.ninja/fact");
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(data);
                var catFact = json.RootElement.GetProperty("fact").GetString();

                var result = new
                {
                    status = "success",
                    user = new
                    {
                        email = "youremail@example.com",
                        name = "Precious Adeoye",
                        stack = "C#/.NET Core"
                    },
                    timestamp = DateTime.UtcNow.ToString("o"),
                    fact = catFact
                };

                return Ok(result);
            }
            catch
            {
                var fallback = new
                {
                    status = "success",
                    user = new
                    {
                        email = "youremail@example.com",
                        name = "Precious Adeoye",
                        stack = "C#/.NET Core"
                    },
                    timestamp = DateTime.UtcNow.ToString("o"),
                    fact = "Could not fetch cat fact at the moment. Try again later!"
                };

                return Ok(fallback);
            }
        }
    }
}

    

