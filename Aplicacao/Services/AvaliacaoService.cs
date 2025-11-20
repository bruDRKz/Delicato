using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<(bool Sucesso, string Mensagem)> CriarAvaliacao(Avaliacao avaliacao)
        {
            await _avaliacaoRepository.CriarAvaliacao(avaliacao);
            return (true, "Avaliação criada com sucesso.");
        }

        public async Task<List<Avaliacao>> ObterTodasAvaliacoes()
        {
            return await _avaliacaoRepository.ObterTodasAvaliacoes();
        }
    }
}
