using System.Threading.Tasks;
using ProAgil.Dominio;

namespace ProAgil.Repositorio
{
    public interface IProAgilRepositorio
    {
        #region Geral
        void Adicionar<T>(T entidade) where T : class;
        void Atualizar<T>(T entidade) where T : class;
        void Deletar<T>(T entidade) where T : class;
        Task<bool> SalvarMudancasAsync();
        #endregion

        #region Eventos
        Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes);
        Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes);
        Task<Evento> SelecionarEventoAsync(int id, bool incluirPalestrantes);
        #endregion

        #region Palestrantes
        Task<Palestrante[]> ObterTodosPalestrantesPorNomeAsync(bool incluirEventos);
        Task<Palestrante> SelecionarPalestranteAsync(int id, bool incluirEventos);
        #endregion
    }
}