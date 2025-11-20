using DelicatoProject.Models;

namespace DelicatoProject.Infraestrutura.Interfaces
{
    public interface IAvaliacaoRepository
    {
        public Task CriarAvaliacao(Avaliacao avaliacao);
        public Task<List<Avaliacao>> ObterTodasAvaliacoes();
    }
}
