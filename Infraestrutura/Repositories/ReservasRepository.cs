using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;
using DelicatoProject.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DelicatoProject.Infraestrutura.Repositories
{
    public class ReservasRepository : IReservasRepository
    {
        private readonly DelicatoContext _context;
        public ReservasRepository(DelicatoContext context) => _context = context;

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

        public async Task<List<Reservas>> ObterReservasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            var listaReservas = await _context.Reservas.ToListAsync();

            var filtrado = listaReservas
                .Where(r =>
                {
                    if (string.IsNullOrWhiteSpace(r.DataReserva))
                        return false;

                    DateTime data;

                    // tenta dd/MM/yyyy primeiro
                    if (DateTime.TryParseExact(r.DataReserva, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out data))
                    {
                        // OK
                    }
                    // tenta yyyy-MM-dd
                    else if (DateTime.TryParseExact(r.DataReserva, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out data))
                    {
                        // OK
                    }
                    else
                    {
                        // formato inválido → ignora ou lança erro
                        return false;
                    }

                    return data >= dataInicio && data <= dataFim;
                })
                .OrderBy(r => r.HoraReserva)
                .ToList();

            return filtrado;
        }

        public async Task<List<Reservas>> ObterReservasPorUsuario(int idUsuario)
        {
            return await _context.Reservas
                .Where(r => r.IdUsuario == idUsuario)
                .ToListAsync();
        }
        public async Task<Reservas?> ObterReservaPorDataEUsuario(string data, int idUsuario)
        {
            return await _context.Reservas
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
