using DelicatoProject.Aplicacao.DTOs.Cardapio;
using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Interfaces
{
    public interface ICardapioService
    {
        public Task<CardapioCompletoDTO> ObterCardapioCompleto();
        public Task<List<CardapioBebidas>> ListarBebidas();
        public Task<(bool Sucesso, string Mensagem)> AdicionarBebida(CardapioBebidas bebida);
        public Task<(bool Sucesso, string Mensagem)> EditarBebida(CardapioBebidas bebida);
        public Task<(bool Sucesso, string Mensagem)> ExcluirBebida(int bebidaId);
        public Task<CardapioBebidas?> RetornaBebidaPorId(int idBebida);
        public Task<List<CardapioComidas>> ListarComidas();
        public Task<(bool Sucesso, string Mensagem)> AdicionarComida(CardapioComidas comida);
        public Task<(bool Sucesso, string Mensagem)> EditarComida(CardapioComidas comida);
        public Task<(bool Sucesso, string Mensagem)> ExcluirComida(int comidaId);
        public Task<CardapioComidas?> RetornaComidaPorId(int idComida);        
    }
}
