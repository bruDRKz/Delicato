using DelicatoProject.Aplicacao.DTOs.Reservas;
using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DelicatoProject.Controllers
{
    public class ReservasController : Controller
    {
        private readonly IReservasService _reservasService;
        public ReservasController(IReservasService reservasService) => _reservasService = reservasService;

        //Ações do usuário - criar reserva, ver reservas, deletar reserva
        [HttpPost]
        public async Task<JsonResult> CriarReserva(ReservasDTO reservaDto)
        {
            try
            {
                var reserva = reservaDto.ToEntity();
                var reservaCriada = await _reservasService.CriarNovaReserva(reserva);
                return Json(new { Sucesso = reservaCriada.Sucesso, Mensagem = reservaCriada.Mensagem });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> ObterReservasPorUsuario(int idUsuario)
        {
            try
            {
                var reservas = await _reservasService.ObterReservasPorUsuario(idUsuario);
                return Json(reservas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete]
        public async Task<JsonResult> CancelarReserva(int idReserva)
        {
            try
            {
                await _reservasService.DeletarReserva(idReserva);
                return Json(new { Sucesso = true, Mensagem = "Reserva cancelada com sucesso." });
            }
            catch (Exception e)
            {
                return Json(new { Sucesso = false, Mensagem = "Não foi possível cancelar a reserva." });
            }
        }

        //Ações do administrador - ver todas reservas, ver reservas por data e horário e concluir reserva

        [HttpPost]
        public async Task<JsonResult> ObterReservasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                List<Reservas> reservas;
                reservas = await _reservasService.ObterReservasPorPeriodo(dataInicio, dataFim);
                return Json(reservas);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        [HttpPut]
        public async Task<JsonResult> ConcluirReserva(int idReserva)
        {
            try
            {
                await _reservasService.ConcluirReserva(idReserva);
                return Json(new { Sucesso = true, Mensagem = "Reserva concluída com sucesso." });
            }
            catch (Exception e)
            {
                return Json(new { Sucesso = false, Mensagem = "Não foi possível concluir a reserva." });
            }
        }
        [HttpPost]
        public async Task<JsonResult> BloquearData(DateTime data, string motivo)
        {
            try
            {
                var (sucesso, mensagem) = await _reservasService.BloquearData(data, motivo);

                if (sucesso)
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
                else
                    return Json(new { Sucesso = sucesso, Mensagem = mensagem });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}