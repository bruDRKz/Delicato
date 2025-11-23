using DelicatoProject.Models;

namespace DelicatoProject.Infraestrutura.Interfaces
{
    public interface IReservasRepository
    {
        public Task CriarNovaReserva(Reservas reserva);
        public Task<Reservas?> ObterReservaPorId(int id);
        public Task<List<Reservas>> ObterReservasPorUsuario(int usuarioId);        
        public Task AtualizarReserva(Reservas reserva);
        public Task DeletarReserva(int id);                
        public Task<List<Reservas>> ObterReservasPorPeriodo(DateTime dataInicio, DateTime dataFim);
        public Task<Reservas?> ObterReservaPorDataEUsuario(string data, int idUsuario);        
        public Task<bool> DataEstaBloqueada(string data);
        public Task BloquearDataReserva(ReservasBloqueio reservasBloqueio);
    }
}
