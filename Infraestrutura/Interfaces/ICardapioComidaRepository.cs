using DelicatoProject.Models;

namespace DelicatoProject.Infraestrutura.Interfaces
{
    public interface ICardapioComidaRepository
    {
        public Task<List<CardapioComidas>> ListarComidas();
        public Task AdicionarComida(CardapioComidas comida);
        public Task EditarComida(CardapioComidas comida);
        public Task ExcluirComida(int comidaId);
        public Task<CardapioComidas?> BuscaComidaPorNome(string nomeComida);
        public Task<CardapioComidas?> RetornaComidaPorId(int idComida);
    }
}
