using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;
using DelicatoProject.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace DelicatoProject.Infraestrutura.Repositories
{
    public class CardapioComidaRepository : ICardapioComidaRepository
    {
        private readonly DelicatoContext _context;
        public CardapioComidaRepository(DelicatoContext context) => _context = context;


        public async Task AdicionarComida(CardapioComidas comida)
        {
            _context.CardapioComidas.Add(comida);
            await  _context.SaveChangesAsync();
        }

        public async Task EditarComida(CardapioComidas comida)
        {
            _context.CardapioComidas.Update(comida);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirComida(int comidaId)
        {
            var comida =  await _context.CardapioComidas.FindAsync(comidaId);
            _context.CardapioComidas.Remove(comida!);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CardapioComidas>> ListarComidas()
        {
            return await _context.CardapioComidas
                .Include(c => c.Categoria)
                .ToListAsync();
        }

        public async Task<CardapioComidas?> BuscaComidaPorNome(string nomeComida)
        {
            return await _context.CardapioComidas
                .Where(c => c.NomeComida.ToLower() == nomeComida.ToLower())
                .FirstOrDefaultAsync();
        }
        public async Task<CardapioComidas?> RetornaComidaPorId(int idComida) => await _context.CardapioComidas.FindAsync(idComida);

    }
}
