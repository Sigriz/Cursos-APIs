using Microsoft.AspNetCore.Mvc;

namespace FraseMotivacional.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrasesMotivacionaisController : ControllerBase
    {
        private static readonly string[] FrasesMotivacionais = new[]
        {
            "Patético", "Faz o L", "Peidei sem querer", "Até que vai", "O cara do lado é ruim", "Gambiarra", "Curso de API no Sabado ?", "Wilton me da 10", "Sou gostoso", "Que sono"
        };

        private readonly ILogger<FrasesMotivacionaisController> _logger;

        public FrasesMotivacionaisController(ILogger<FrasesMotivacionaisController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetFraseMotivacional")]
        public FraseMotivacional Get()
        {
            return new FraseMotivacional
            {
                id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Frase = FrasesMotivacionais[Random.Shared.Next(FrasesMotivacionais.Length)]


            };
        }
    }
}
