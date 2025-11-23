using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;
using DelicatoProject.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DelicatoProject.Infraestrutura.Repositories
{
    public class CardapioBebidaRepository : ICardapioBebidaRepository
    {
        private readonly DelicatoContext _context;
        public CardapioBebidaRepository(DelicatoContext context) => _context = context;

        public async Task<List<CardapioBebidas>> ListarBebidas()
        {
            return await _context.CardapioBebidas
                .Include(c => c.Categoria)
                .ToListAsync();
        }
        public async Task AdicionarBebida(CardapioBebidas bebida)  
        {
            _context.CardapioBebidas.Add(bebida);
            await _context.SaveChangesAsync();
        }
        public async Task EditarBebida(CardapioBebidas bebida) //Mandar na controller o objeto completo com as alterações vindas do front
        { 
            _context.CardapioBebidas.Update(bebida);
            await _context.SaveChangesAsync();
        }
        public async Task ExcluirBebida(int bebidaId)
        {
            var bebida = await _context.CardapioBebidas.FindAsync(bebidaId);            
                _context.CardapioBebidas.Remove(bebida!);
                await _context.SaveChangesAsync();
            
        }

        public async Task<CardapioBebidas?> BuscaBebidaPorNome(string nomeBebida)
        {
            var bebida = await _context.CardapioBebidas
                .Where(b => b.NomeBebida.ToLower() == nomeBebida.ToLower())
                .FirstOrDefaultAsync();

            if (bebida == null) 
                return null;
            
            else 
                return bebida;
        }

        public async Task<CardapioBebidas?> RetornaBebidaPorId(int idBebida) => await _context.CardapioBebidas.FindAsync(idBebida);     
    }
}
