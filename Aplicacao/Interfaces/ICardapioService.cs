using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Interfaces
{
    public interface ICardapioService
    {
        public Task<List<CardapioBebidas>> ListarBebidas();
        public Task AdicionarBebida(CardapioBebidas bebida);
        public Task EditarBebida(CardapioBebidas bebida);
        public Task ExcluirBebida(int bebidaId);
        public Task<CardapioBebidas?> RetornaBebidaPorId(int idBebida);
        public Task<List<CardapioComidas>> ListarComidas();
        public Task AdicionarComida(CardapioComidas comida);
        public Task EditarComida(CardapioComidas comida);
        public Task ExcluirComida(int comidaId);
        public Task<CardapioComidas?> RetornaComidaPorId(int idComida);        
    }
}
