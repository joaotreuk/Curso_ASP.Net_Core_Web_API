using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Dominio;
using ProAgil.Repositorio;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        public IProAgilRepositorio Repo { get; }
        public IMapper Mapeador { get; }

        public EventoController(IProAgilRepositorio repo, IMapper mapeador)
        {
            Repo = repo;
            Mapeador = mapeador;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Evento[] eventos = await Repo.ObterTodosEventosAsync(true);
                EventoDto[] resultado = Mapeador.Map<EventoDto[]>(eventos);

                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou! {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                IFormFile arquivo = Request.Form.Files[0];
                string nomePasta = Path.Combine("Resources", "Images");
                string lugarParaSalvar = Path.Combine(Directory.GetCurrentDirectory(), nomePasta);

                if (arquivo.Length > 0)
                {
                    string nomeArquivo = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName;
                    string caminhoCompleto = Path.Combine(lugarParaSalvar, nomeArquivo.Replace("\"", "").Trim());

                    using(FileStream stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(stream);
                    }

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou! {ex.Message}");
            }

            return BadRequest("Erro ao tentar realizar upload!");
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                Evento evento = await Repo.SelecionarEventoAsync(EventoId, true);
                EventoDto resultado = Mapeador.Map<EventoDto>(evento);

                return Ok(resultado);
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
                EventoDto[] resultado = Mapeador.Map<EventoDto[]>(eventos);
                return Ok(resultado);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto modelo)
        {
            try
            {
                Evento evento = Mapeador.Map<Evento>(modelo);

                Repo.Adicionar(evento);

                if (await Repo.SalvarMudancasAsync())
                {
                    return Created($"/api/evento/{modelo.Id}", Mapeador.Map<EventoDto>(evento));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou! {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, EventoDto modelo)
        {
            try
            {
                Evento evento = await Repo.SelecionarEventoAsync(EventoId, false);
                if (evento == null) return NotFound();

                Mapeador.Map(modelo, evento);

                Repo.Atualizar(evento);

                if (await Repo.SalvarMudancasAsync())
                {
                    return Created($"/api/evento/{modelo.Id}", Mapeador.Map<EventoDto>(evento));
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