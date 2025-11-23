using DelicatoProject.Aplicacao.DTOs.Cardapio;
using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DelicatoProject.Controllers
{
    public class CardapioController : Controller
    {
        private readonly ICardapioService _cardapioService;
        public CardapioController(ICardapioService cardapioService)
        {
            _cardapioService = cardapioService;
        }

        public IActionResult Cardapio()
        {
            return View("Cardapio");
        }

        [HttpGet]
        public async Task<JsonResult> ListarCardapioCompleto()
        {
            try
            {
                var cardapioCompleto = await _cardapioService.ObterCardapioCompleto();
                return Json(cardapioCompleto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> ListarBebidas()
        {
            try
            {
                List<CardapioBebidas> bebidas = await _cardapioService.ListarBebidas();
                return Json(bebidas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        public async Task<JsonResult> ListarComidas()
        {
            try
            {
                List<CardapioComidas> comidas = await _cardapioService.ListarComidas();
                return Json(comidas);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AdicionarItemCardapio(string nome, decimal preco, int categoria, string descricao, string tipo)
        {
            try
            {
                if (tipo == "Comida")
                {
                    CardapioComidaDTO comidaDTO = new CardapioComidaDTO
                    {
                        NomeComida = nome,
                        Preco = preco,
                        IdCategoria = categoria,
                        DescricaoComida = descricao,
                        ImagemUrl = "",
                        Disponivel = true
                    };
                    (bool sucesso, string mensagem) = await _cardapioService.AdicionarComida(comidaDTO);
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
                }
                else if (tipo == "Bebida")
                {
                    CardapioBebidaDTO bebidaDTO = new CardapioBebidaDTO
                    {
                        NomeBebida = nome,
                        Preco = preco,
                        IdCategoria = categoria,
                        DescricaoBebida = "",
                        ImagemUrl = "",
                        Disponivel = true
                    };
                    (bool sucesso, string mensagem) = await _cardapioService.AdicionarBebida(bebidaDTO);
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
                }

                return Json(new { Sucesso = false, Mensagem = "Tipo inválido." });
            }
            catch (Exception e)
            {
                return Json(new { Sucesso = false, Mensagem = "Erro inesperado: " + e.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> EditarItemCardapio(int id, string nome, decimal preco, int categoria, string descricao, bool disponivel, string tipo)
        {
            try
            {
                if (tipo == "Comida")
                {
                    CardapioComidaDTO comidaDTO = new CardapioComidaDTO
                    {
                        IdComida = id,
                        NomeComida = nome,
                        Preco = preco,
                        IdCategoria = categoria,
                        DescricaoComida = descricao,
                        ImagemUrl = "",
                        Disponivel = disponivel
                    };
                    (bool sucesso, string mensagem) = await _cardapioService.EditarComida(comidaDTO);
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
                }
                else if (tipo == "Bebida")
                {
                    CardapioBebidaDTO bebidaDTO = new CardapioBebidaDTO
                    {
                        IdBebida = id,
                        NomeBebida = nome,
                        Preco = preco,
                        IdCategoria = categoria,
                        DescricaoBebida = "",
                        ImagemUrl = "",
                        Disponivel = disponivel
                    };
                    (bool sucesso, string mensagem) = await _cardapioService.EditarBebida(bebidaDTO);
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
                }

                return Json(new { Sucesso = false, Mensagem = "Tipo inválido." });
            }
            catch (Exception e)
            {
                return Json(new { Sucesso = false, Mensagem = "Erro inesperado: " + e.Message });
            }
        }

        [HttpDelete]
        public async Task<JsonResult> RemoverItemCardapio(int id, string tipo)
        {
            try
            {
                if (tipo == "Comida")
                {
                    (bool sucesso, string mensagem) = await _cardapioService.ExcluirComida(id);
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
                }
                else if (tipo == "Bebida")
                {
                    (bool sucesso, string mensagem) = await _cardapioService.ExcluirBebida(id);
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
                }
                return Json(new { Sucesso = false, Mensagem = "Tipo inválido." });
            }
            catch (Exception e)
            {
                return Json(new { Sucesso = false, Mensagem = "Erro inesperado: " + e.Message });
            }
        }
    }
}
