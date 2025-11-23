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

        public (bool Autenticado, int? Id) AutenticarUsuario(string telefone, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Telefone == telefone.Trim());

            if (usuario!.VerificarSenha(senha))
                return (true, usuario.Id); 
                      
            return (false, null);            
        }

        public (bool Existe, int? Id) JaExisteUsuarioComTelefone(string telefone)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Telefone == telefone.Trim());

            if (usuario != null)
                return (true, usuario.Id);
            return (false, null);

        }
    }
}
