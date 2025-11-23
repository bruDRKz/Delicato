using DelicatoProject.Aplicacao.DTOs.Avaliacao;
using DelicatoProject.Aplicacao.DTOs.Reservas;
using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DelicatoProject.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly IAvaliacaoService _avaliacaoService;
        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }        

        [HttpPost]
        public async Task<JsonResult> EnviarAvaliacao(AvaliacaoDTO avaliacaoDTO)
        {
            try
            {
                var avaliacao = avaliacaoDTO.ConvertToEntity();
                var avaliacaoCriada = await _avaliacaoService.CriarAvaliacao(avaliacao);
                return Json(new { Sucesso = avaliacaoCriada.Sucesso, Mensagem = avaliacaoCriada.Mensagem });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //[HttpGet]
        //public async Task<JsonResult> ObterAvaliacoes()
        //{
        //    try
        //    {
        //        var avaliacoes = await _avaliacaoService.ObterTodasAvaliacoes();
        //        return avaliacoes;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
}
