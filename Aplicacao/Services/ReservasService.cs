using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Services
{
    public class ReservasService : IReservasService
    {
        private readonly IReservasRepository _reservasRepository;
        public ReservasService(IReservasRepository reservasRepository) => _reservasRepository = reservasRepository;
        public async Task<(bool Sucesso, string Mensagem)> CriarNovaReserva(Reservas reserva)
        {
            var podeCadastrar = ReservaPodeSerCadastrada(reserva);
            if (podeCadastrar.Sucesso)
            {
                await _reservasRepository.CriarNovaReserva(reserva);
                return podeCadastrar;
            }
            else            
                return podeCadastrar;
        }        
        public Task<List<Reservas>> ObterReservasPorPeriodo(DateTime dataInicio, DateTime dataFim) => _reservasRepository.ObterReservasPorPeriodo(dataInicio, dataFim);        
        public Task<List<Reservas>> ObterReservasPorUsuario(int idUsuario) => _reservasRepository.ObterReservasPorUsuario(idUsuario);       
        public async Task<(bool Sucesso, string Mensagem)> DeletarReserva(int idReserva)
        {
            var reservaExistente = await _reservasRepository.ObterReservaPorId(idReserva);
            if (reservaExistente != null)
            {
                await _reservasRepository.DeletarReserva(idReserva);
                return (true, "Reserva deletada com sucesso.");
            }
            else
                return (false, "Reserva não encontrada.");
        }

        public async Task<(bool Sucesso, string Mensagem)> ConcluirReserva(int idReserva)
        {
            var reservaExistente = await _reservasRepository.ObterReservaPorId(idReserva);
            if (reservaExistente != null)
            {
                reservaExistente.ReservaConcluida = true;
                await _reservasRepository.AtualizarReserva(reservaExistente);
                return (true, "Reserva concluída com sucesso.");
            }
            else
                return (false, "Reserva não encontrada.");
        }
        public async Task<(bool Sucesso, string Mensagem)> BloquearData(DateTime data, string motivo)
        {
            bool dataJaEstaBloqueada = await _reservasRepository.DataEstaBloqueada(data.ToString("dd/MM/yyyy"));
            if (dataJaEstaBloqueada)
                return (false, "Data já está bloqueada!");

            var reservasBloqueio = new ReservasBloqueio
            {
                DataBloqueio = data.ToString("dd/MM/yyyy"),
                MotivoBloqueio = motivo,
                CriadoEm = DateTime.Now.ToString("dd/MM/yyyy"),
                Ativo = true
            };

            await _reservasRepository.BloquearDataReserva(reservasBloqueio);
            return (true, "Data bloqueada com sucesso!");
        }

        private (bool Sucesso, string Mensagem) ReservaPodeSerCadastrada(Reservas reserva)
        {
            bool dataBloqueada = _reservasRepository.DataEstaBloqueada(reserva.DataReserva).Result;
            if (dataBloqueada)
                return (false, "Data indisponível para novas reservas.");

            var reservaExistente = _reservasRepository.ObterReservaPorDataEUsuario(reserva.DataReserva, reserva.IdUsuario).Result;
            if (reservaExistente != null)
                return (false, "Usuário ja possuí uma reserva para esta data.");

            return (true, "Reserva criada com sucesso.");
        }
    }
}
