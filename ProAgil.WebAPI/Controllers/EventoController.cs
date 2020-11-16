using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Dominio;
using ProAgil.Repositorio;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        public IProAgilRepositorio Repo { get; }

        public EventoController(IProAgilRepositorio repo)
        {
            Repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Evento[] eventos = await Repo.ObterTodosEventosAsync(true);
                return Ok(eventos);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                Evento evento = await Repo.SelecionarEventoAsync(EventoId, true);
                return Ok(evento);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpGet("obterPorTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                Evento[] eventos = await Repo.ObterTodosEventosPorTemaAsync(tema, true);
                return Ok(eventos);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
            try
            {
                Repo.Adicionar(evento);

                if (await Repo.SalvarMudancasAsync())
                {
                    return Created($"/api/evento/{evento.Id}", evento);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento modelo)
        {
            try
            {
                Evento evento = await Repo.SelecionarEventoAsync(EventoId, false);

                if (evento == null) return NotFound();

                Repo.Atualizar(modelo);

                if (await Repo.SalvarMudancasAsync())
                {
                    return Created($"/api/evento/{modelo.Id}", modelo);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Deletar(int EventoId)
        {
            try
            {
                Evento evento = await Repo.SelecionarEventoAsync(EventoId, false);
                if (evento == null) return NotFound();

                Repo.Deletar(evento);

                if (await Repo.SalvarMudancasAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }
    }
}