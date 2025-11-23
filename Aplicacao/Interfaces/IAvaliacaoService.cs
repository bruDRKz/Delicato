using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Interfaces
{
    public interface IAvaliacaoService
    {
        public Task<(bool Sucesso, string Mensagem)> CriarAvaliacao(Avaliacao avaliacao);
        public Task<List<Avaliacao>> ObterTodasAvaliacoes();
    }
}
