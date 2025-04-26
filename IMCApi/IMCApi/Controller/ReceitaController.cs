using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace IMCApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceitaController : ControllerBase
    {
        private static List<Receita> receitas = new List<Receita>
        {
            new Receita
            {
                ReceitaId = 1, NomeReceita = "Salada de Frango",
                Tipo = "Fit", Ingrendientes = "Frango e Alface"
            },
            new Receita
            {
                ReceitaId = 2, NomeReceita = "Bolo de Chocolate",
                Tipo = "Normal", Ingrendientes = "Farinha, ovo, leite e chocolate"
            },
            new Receita
            {
                ReceitaId = 3, NomeReceita = "Smoothie Verde",
                Tipo = "Fit", Ingrendientes = "Couve e Limão"
            },
            new Receita
            {
                ReceitaId = 4, NomeReceita = "Pizza Calabresa",
                Tipo = "Normal", Ingrendientes = "Molho de tomate, farinha, ovo e calabresa"
            },
        };

        private readonly ILogger<ReceitaController> _logger;

        public ReceitaController(ILogger<ReceitaController> logger)
        {
            _logger = logger;
        }

        [HttpGet(" imc ", Name = "GetReceita")]
        public IActionResult Get(double imc)
        {

            Random rand = new Random();
            if (imc < 25)
            {
                var receitasNormal = receitas
                .Where(rec => rec.Tipo == "Normal").ToList();
                Receita receitaAleatoria =
                    receitasNormal[rand.Next(receitasNormal.Count)];

                return new JsonResult(receitaAleatoria);
            }
            else
            {
                var receitasFit = receitas
                    .Where(r => r.Tipo == "Fit").ToList();
                Receita receitaAleatoria =
                    receitasFit[rand.Next(receitasFit.Count)];

                return new JsonResult(receitaAleatoria);
            }
        }

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(receitas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Receita novaReceita)
        {
            if (novaReceita == null ||
                string.IsNullOrEmpty(novaReceita.NomeReceita))
            {
                return BadRequest("Dados inválidos");
            }

            int novoId = receitas.Max(r => r.ReceitaId) + 1;
            novaReceita.ReceitaId = novoId;
            receitas.Add(novaReceita);

            return Ok(novaReceita);
        }
        [HttpPut("Id")]
        public IActionResult Put(int id, [FromBody] Receita receitaAtualizada)
        {
            if (receitaAtualizada == null)
            {
                return BadRequest("Dados inválidos");
            }
            var receitaExistente = receitas
                .FirstOrDefault(r => r.ReceitaId == id);
            if (receitaExistente == null)
            {
                return NotFound("Receita não encontrada. ");
            }

            receitaExistente.NomeReceita = receitaAtualizada.NomeReceita;
            receitaExistente.Tipo = receitaAtualizada.Tipo;
            receitaExistente.Ingrendientes = receitaAtualizada.Ingrendientes;

            return Ok(receitaExistente);
        }

        [HttpDelete(" id ")]
        public IActionResult Delete(int id)
        {
            var receita = receitas.FirstOrDefault(r => r.ReceitaId == id);
            if (receita == null)
                return NotFound("Receita não encontrada");

            receitas.Remove(receita);

            return NoContent();
        }
    }
}