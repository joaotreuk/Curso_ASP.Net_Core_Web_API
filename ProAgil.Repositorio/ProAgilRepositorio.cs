using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Dominio;

namespace ProAgil.Repositorio
{
    public class ProAgilRepositorio : IProAgilRepositorio
    {
        public ProAgilContexto Contexto { get; }

        public ProAgilRepositorio(ProAgilContexto contexto)
        {
            Contexto = contexto;
            Contexto.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region Geral
        public void Adicionar<T>(T entidade) where T : class
        {
            Contexto.Add(entidade);
        }

        public void Atualizar<T>(T entidade) where T : class
        {
            Contexto.Update(entidade);
        }

        public void Deletar<T>(T entidade) where T : class
        {
            Contexto.Remove(entidade);
        }

        public async Task<bool> SalvarMudancasAsync()
        {
            return (await Contexto.SaveChangesAsync()) > 0;
        }
        #endregion

        #region Eventos
        public async Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = Contexto.Eventos.Include(evento => evento.Lotes).Include(evento => evento.RedesSociais);

            if (incluirPalestrantes)
            {
                query = query.Include(evento => evento.EventoPalestrantes).ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(evento => evento.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = Contexto.Eventos.Include(evento => evento.Lotes)
                .Include(evento => evento.RedesSociais);

            if (incluirPalestrantes)
            {
                query = query.Include(evento => evento.EventoPalestrantes)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderByDescending(evento => evento.DataEvento)
                .Where(evento => evento.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> SelecionarEventoAsync(int id, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = Contexto.Eventos.Include(evento => evento.Lotes).Include(evento => evento.RedesSociais);

            if (incluirPalestrantes)
            {
                query = query.Include(evento => evento.EventoPalestrantes).ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().Where(evento => evento.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        #endregion

        #region Palestrantes
        public async Task<Palestrante[]> ObterTodosPalestrantesPorNomeAsync(bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = Contexto.Palestrantes.Include(palestrante => palestrante.RedesSociais);

            if (incluirEventos)
            {
                query = query.Include(palestrante => palestrante.Eventos).ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(palestrante => palestrante.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> SelecionarPalestranteAsync(int id, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = Contexto.Palestrantes.Include(palestrante => palestrante.RedesSociais);

            if (incluirEventos)
            {
                query = query.Include(palestrante => palestrante.Eventos).ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking().Where(palestrante => palestrante.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        #endregion
    }
}