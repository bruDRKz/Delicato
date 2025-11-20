using DelicatoProject.Models;

namespace DelicatoProject.Infraestrutura.Interfaces
{
    public interface IReservasRepository
    {
        public Task CriarNovaReserva(Reservas reserva);
        public Task<Reservas?> ObterReservaPorId(int id);
        public Task<List<Reservas>> ObterReservasPorUsuario(int usuarioId);
        public Task<List<Reservas>> ObterTodasReservas();
        public Task AtualizarReserva(Reservas reserva);
        public Task DeletarReserva(int id);        
        public Task<List<Reservas>> ObterReservasPorDataGeral(DateTime data);
        public Task<List<Reservas>> ObterReservasPorDataEHorario(DateTime data, string horario);
        public Task<Reservas?> ObterReservaPorDataEUsuario(string data, int idUsuario);        
        public Task<bool> DataEstaBloqueada(string data);
        public Task BloquearDataReserva(ReservasBloqueio reservasBloqueio);
    }
}
