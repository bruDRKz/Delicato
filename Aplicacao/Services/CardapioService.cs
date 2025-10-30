using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Services
{
    public class CardapioService : ICardapioService
    {
        private readonly ICardapioBebidaRepository _cardapioBebidaRepository;
        private readonly ICardapioComidaRepository _cardapioComidaRepository;
        public CardapioService(ICardapioBebidaRepository cardapioBebidaRepository, ICardapioComidaRepository cardapioComidaRepository)
        {
            _cardapioBebidaRepository = cardapioBebidaRepository;
            _cardapioComidaRepository = cardapioComidaRepository;
        }

        //Bebidas
        public async Task<List<CardapioBebidas>> ListarBebidas() => await _cardapioBebidaRepository.ListarBebidas();
        public async Task AdicionarBebida(CardapioBebidas bebida) => await _cardapioBebidaRepository.AdicionarBebida(bebida);
        public async Task EditarBebida(CardapioBebidas bebida) => await _cardapioBebidaRepository.EditarBebida(bebida);
        public async Task ExcluirBebida(int bebidaId) => await _cardapioBebidaRepository.ExcluirBebida(bebidaId);
        public async Task<CardapioBebidas?> RetornaBebidaPorId(int idBebida) => await _cardapioBebidaRepository.RetornaBebidaPorId(idBebida);

        //Comidas
        public async Task<List<CardapioComidas>> ListarComidas() => await _cardapioComidaRepository.ListarComidas();
        public async Task AdicionarComida(CardapioComidas comida) => await _cardapioComidaRepository.AdicionarComida(comida);
        public async Task EditarComida(CardapioComidas comida) => await _cardapioComidaRepository.EditarComida(comida);
        public async Task ExcluirComida(int comidaId) => await _cardapioComidaRepository.ExcluirComida(comidaId);
        public async Task<CardapioComidas?> RetornaComidaPorId(int idComida) => await _cardapioComidaRepository.RetornaComidaPorId(idComida);

    }
}
