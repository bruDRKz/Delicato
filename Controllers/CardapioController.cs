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

        public IActionResult CardapioCompleto()
        {
            return View("Cardapio");
        }

        [HttpGet]
        public async Task<IActionResult> ListarBebidas()
        {
            try
            {
                List<CardapioBebidas> bebidas = await _cardapioService.ListarBebidas();
                return Ok(bebidas);
                //Retorna um status 200 com a lista de bebidas no corpo da resposta
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ListarComidas()
        {
            try
            {
                List<CardapioComidas> comidas = await _cardapioService.ListarComidas();
                return Ok(comidas);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
