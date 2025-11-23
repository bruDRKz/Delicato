using DelicatoProject.Models;

namespace DelicatoProject.Infraestrutura.Interfaces
{
    public interface ICardapioBebidaRepository
    {
        public Task<List<CardapioBebidas>> ListarBebidas();
        public Task AdicionarBebida(CardapioBebidas bebida);
        public Task EditarBebida(CardapioBebidas bebida);
        public Task ExcluirBebida(int bebidaId);
        public Task<CardapioBebidas?> BuscaBebidaPorNome(string nomeBebida);
        public Task<CardapioBebidas?> RetornaBebidaPorId(int idBebida);
    }
}
