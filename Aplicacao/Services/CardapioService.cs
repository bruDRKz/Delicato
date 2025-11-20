using DelicatoProject.Aplicacao.DTOs.Cardapio;
using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;
using System.ComponentModel.Design;

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

        //-----------------------Cardapio completo-----------------------
        public async Task<CardapioCompletoDTO> ObterCardapioCompleto()
        {
            var bebidas = await _cardapioBebidaRepository.ListarBebidas();
            var comidas = await _cardapioComidaRepository.ListarComidas();

            var dtoCompleta = new CardapioCompletoDTO
            {
                BebidasDTO = bebidas.Select(b => new CardapioBebidaDTO
                {
                    IdBebida = b.IdBebida,
                    NomeBebida = b.NomeBebida,
                    DescricaoBebida = b.DescricaoBebida,
                    Preco = b.Preco,
                    IdCategoria = b.IdCategoria,
                    ImagemUrl = b.ImagemUrl,
                    Disponivel = b.Disponivel
                }).ToList(),

                ComidasDTO = comidas.Select(c => new CardapioComidaDTO
                {
                    IdComida = c.IdComida,
                    NomeComida = c.NomeComida,
                    DescricaoComida = c.DescricaoComida,
                    Preco = c.Preco,
                    IdCategoria = c.IdCategoria,
                    ImagemUrl = c.ImagemUrl,
                    Disponivel = c.Disponivel
                }).ToList()
            };

            return dtoCompleta;
        }


        //-----------------------Bebidas-----------------------
        public async Task<List<CardapioBebidas>> ListarBebidas() => await _cardapioBebidaRepository.ListarBebidas();

        public async Task<(bool Sucesso, string Mensagem)>AdicionarBebida(CardapioBebidas bebida)
        { 
            CardapioBebidas? bebidaExistente = await _cardapioBebidaRepository.BuscaBebidaPorNome(bebida.NomeBebida);

            if (bebidaExistente != null)
                return (false, "Bebida ja existente no cardapio");
            
            await _cardapioBebidaRepository.AdicionarBebida(bebida);
            return (true, "Bebida adicionada com sucesso");
        }
        public async Task<(bool Sucesso, string Mensagem)> EditarBebida(CardapioBebidas novaBebida)
        { 
            CardapioBebidas? bebidaExistente = await _cardapioBebidaRepository.RetornaBebidaPorId(novaBebida.IdBebida);
            if (bebidaExistente == null)
                return (false, "Bebida nao encontrada no cardapio");

            bebidaExistente.NomeBebida = novaBebida.NomeBebida;
            bebidaExistente.DescricaoBebida = novaBebida.DescricaoBebida;
            bebidaExistente.Preco = novaBebida.Preco;
            bebidaExistente.IdCategoria = novaBebida.IdCategoria;
            bebidaExistente.ImagemUrl = novaBebida.ImagemUrl;
            bebidaExistente.Disponivel = novaBebida.Disponivel;
            await _cardapioBebidaRepository.EditarBebida(bebidaExistente);
            return (true, "Bebida editada com sucesso");
        }
        public async Task<(bool Sucesso, string Mensagem)> ExcluirBebida(int bebidaId)
        {
            CardapioBebidas? bebidaExistente = await _cardapioBebidaRepository.RetornaBebidaPorId(bebidaId);
            if (bebidaExistente == null)
                return (false, "Bebida nao encontrada no cardapio");
            await _cardapioBebidaRepository.ExcluirBebida(bebidaId);
            return (true, "Bebida excluida com sucesso");
        }

        public async Task<CardapioBebidas?> RetornaBebidaPorId(int idBebida) => await _cardapioBebidaRepository.RetornaBebidaPorId(idBebida);


        //-----------------------Comidas-----------------------
        public async Task<List<CardapioComidas>> ListarComidas() => await _cardapioComidaRepository.ListarComidas();

        public async Task<(bool Sucesso, string Mensagem)> AdicionarComida(CardapioComidas comida)
        {
            CardapioComidas? comidaExistente = await _cardapioComidaRepository.BuscaComidaPorNome(comida.NomeComida);
            if (comidaExistente != null)
                return (false, "Comida ja existente no cardapio");
            await _cardapioComidaRepository.AdicionarComida(comida);
            return (true, "Comida adicionada com sucesso");
        }
        public async Task<(bool Sucesso, string Mensagem)> EditarComida(CardapioComidas novaComida)
        {
            CardapioComidas? comidaExistente = await _cardapioComidaRepository.RetornaComidaPorId(novaComida.IdComida);
            if (comidaExistente == null)
                return (false, "Comida nao encontrada no cardapio");
            comidaExistente.NomeComida = novaComida.NomeComida;
            comidaExistente.DescricaoComida = novaComida.DescricaoComida;
            comidaExistente.Preco = novaComida.Preco;
            comidaExistente.IdCategoria = novaComida.IdCategoria;
            comidaExistente.ImagemUrl = novaComida.ImagemUrl;
            comidaExistente.Disponivel = novaComida.Disponivel;
            await _cardapioComidaRepository.EditarComida(comidaExistente);
            return (true, "Comida editada com sucesso");
        }
        public async Task<(bool Sucesso, string Mensagem)> ExcluirComida(int comidaId)
        {
            CardapioComidas? comidaExistente = await _cardapioComidaRepository.RetornaComidaPorId(comidaId);
            if (comidaExistente == null)
                return (false, "Comida nao encontrada no cardapio");
            await _cardapioComidaRepository.ExcluirComida(comidaId);
            return (true, "Comida excluida com sucesso");
        }

        public async Task<CardapioComidas?> RetornaComidaPorId(int idComida) => await _cardapioComidaRepository.RetornaComidaPorId(idComida);

    }
}
