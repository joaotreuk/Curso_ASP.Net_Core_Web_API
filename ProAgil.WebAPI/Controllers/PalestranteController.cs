using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Dominio;
using ProAgil.Repositorio;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PalestranteController : ControllerBase
    {
        public IProAgilRepositorio Repo { get; }

        public PalestranteController(IProAgilRepositorio repo)
        {
            Repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Palestrante[] palestrantes = await Repo.ObterTodosPalestrantesPorNomeAsync(false);
                return Ok(palestrantes);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                Palestrante palestrante = await Repo.SelecionarPalestranteAsync(PalestranteId, false);
                return Ok(palestrante);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante palestrante)
        {
            try
            {
                Repo.Adicionar(palestrante);

                if (await Repo.SalvarMudancasAsync())
                {
                    return Created($"/api/palestrante/{palestrante.Id}", palestrante);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante modelo)
        {
            try
            {
                Palestrante palestrante = await Repo.SelecionarPalestranteAsync(PalestranteId, false);

                if (palestrante == null) return NotFound();

                Repo.Atualizar(modelo);

                if (await Repo.SalvarMudancasAsync())
                {
                    return Created($"/api/palestrante/{modelo.Id}", modelo);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int PalestranteId)
        {
            try
            {
                Palestrante palestrante = await Repo.SelecionarPalestranteAsync(PalestranteId, false);
                if (palestrante == null) return NotFound();

                Repo.Deletar(palestrante);

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