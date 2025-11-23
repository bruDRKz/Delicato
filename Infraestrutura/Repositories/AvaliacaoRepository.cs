using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;
using DelicatoProject.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DelicatoProject.Infraestrutura.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly DelicatoContext _context;
        public AvaliacaoRepository(DelicatoContext context)
        {
            _context = context;
        }
        public async Task CriarAvaliacao(Avaliacao avaliacao)
        {
            await _context.Avaliacao.AddAsync(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Avaliacao>> ObterTodasAvaliacoes()
        {
            var avaliacoes = await _context.Avaliacao.ToListAsync();
            if (avaliacoes != null && avaliacoes.Count > 0)
                return avaliacoes;
            else
                return new List<Avaliacao>();
        }
    }
}
