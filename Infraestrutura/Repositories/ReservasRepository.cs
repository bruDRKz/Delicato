using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;
using DelicatoProject.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace DelicatoProject.Infraestrutura.Repositories
{
    public class ReservasRepository : IReservasRepository
    {
        private readonly DelicatoContext _context;
        public ReservasRepository(DelicatoContext context) =>  _context = context;

        public async Task AtualizarReserva(Reservas reserva)
        {
           _context.Reservas.Update(reserva!);
             await _context.SaveChangesAsync();
        }

        public async Task CriarNovaReserva(Reservas reserva)
        {
            _context.Reservas.Add(reserva);
             await _context.SaveChangesAsync();
        }

        public async Task DeletarReserva(int idReserva)
        {
            var reserva = _context.Reservas.Find(idReserva);
            _context.Reservas.Remove(reserva!);
             await _context.SaveChangesAsync();
        }

        public async Task<Reservas?> ObterReservaPorId(int id)
        {
            return await _context.Reservas
                .Where(r => r.IdReserva == id)
                .FirstOrDefaultAsync();
        }       

        public async Task<List<Reservas>> ObterTodasReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        public async Task<List<Reservas>> ObterReservasPorDataGeral(DateTime data)
        {
            return await _context.Reservas
                .Where(r => r.DataReserva == data.ToString("dd/MM/yyyy"))
                .ToListAsync();
        }

        public async Task<List<Reservas>> ObterReservasPorDataEHorario(DateTime data, string horario)
        {
            var listaReservas = await _context.Reservas
                .Where(r => r.DataReserva == data.ToString("dd/MM/yyyy") && r.HoraReserva == horario)
                .OrderBy(r => r.HoraReserva)
                .ToListAsync();

            return listaReservas;
        }

        public async Task<List<Reservas>> ObterReservasPorUsuario(int idUsuario)
        { 
            return await _context.Reservas
                .Where(r => r.IdUsuario == idUsuario)
                .ToListAsync();
        }
        public async Task<Reservas?> ObterReservaPorDataEUsuario(string data, int idUsuario)
        {
            return  await _context.Reservas
                .Where(r => r.DataReserva == data && r.IdUsuario == idUsuario)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> DataEstaBloqueada(string data)
        { 
            return await _context.ReservasBloqueio
                .AnyAsync(b => b.DataBloqueio == data && b.Ativo);
        }
        public async Task BloquearDataReserva(ReservasBloqueio reservasBloqueio)
        {
            _context.ReservasBloqueio.Add(reservasBloqueio);
            await _context.SaveChangesAsync();
        }
    }
}
