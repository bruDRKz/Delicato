using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models.Context;

namespace DelicatoProject.Infraestrutura.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DelicatoContext _context;
        public UsuarioRepository(DelicatoContext context)
        {
            _context = context;
        }
        public void AdicionarUsuario(Models.Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}
