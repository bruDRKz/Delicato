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
        public async Task<IActionResult> CriarReserva([FromBody] ReservasDTO reservaDto)
        {
            try
            {
                var reserva = reservaDto.ToEntity();
                var reservaCriada = await _reservasService.CriarNovaReserva(reserva);
                return Ok(reservaCriada);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObterReservasPorUsuario(int idUsuario)
        {
            try
            {
                var reservas = await _reservasService.ObterReservasPorUsuario(idUsuario);
                return Ok(reservas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeletarReserva(int idReserva)
        {
            try
            {
                await _reservasService.DeletarReserva(idReserva);
                return Ok(new { Mensagem = "Reserva deletada com sucesso." });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Ações do administrador - ver todas reservas, ver reservas por data e horário e concluir reserva
        [HttpGet]
        public async Task<IActionResult> ObterTodasReservasGeral()
        {
            try
            {
                var reservas = await _reservasService.ObterTodasReservasGeral();
                return Ok(reservas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterReservaPorDataEHorario(DateTime dataReserva, string? horario)
        {
            try
            {
                List<Reservas> reservas;
                if (!horario.IsNullOrEmpty())
                {
                    reservas = await _reservasService.ObterReservasPorDataEHorario(dataReserva, horario);
                    return Ok(reservas);
                }
                else
                {
                    reservas = await _reservasService.ObterReservasPorDataGeral(dataReserva);
                    return Ok(reservas);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        [HttpPut]
        public async Task<IActionResult> ConcluirReserva(int idReserva)
        {
            try
            {
               await _reservasService.ConcluirReserva(idReserva);
               return Ok(new { Mensagem = "Reserva concluída com sucesso." });              
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> BloquearData(DateTime data, string motivo)
        {
            try
            {
                var (sucesso, mensagem) = await _reservasService.BloquearData(data, motivo);

                if (sucesso)
                    return Ok(new { Mensagem = mensagem });
                else
                    return BadRequest(new { Mensagem = mensagem });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Mensagem = "Erro interno: " + e.Message });
            }
        }

    }
}