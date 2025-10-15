using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Infraestrutura.Interfaces;
using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(Infraestrutura.Interfaces.IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public void AdicionarUsuario(Usuario usuario)
        {
            _usuarioRepository.AdicionarUsuario(usuario);
        }
    }
}
