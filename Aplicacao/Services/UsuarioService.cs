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
        public (bool Existe, int? Id) JaExisteUsuarioComTelefone(string telefone)
        {
            var usuarioExiste = _usuarioRepository.JaExisteUsuarioComTelefone(telefone);

            if (usuarioExiste.Existe)
                return (true, usuarioExiste.Id);
            else
                return (false, null);
        }

        public (bool Autenticado, int? Id) AutenticarUsuario(string telefone, string senha)
        { 
            var autenticacao = _usuarioRepository.AutenticarUsuario(telefone, senha);
            if (autenticacao.Autenticado)
                return (true, autenticacao.Id);
            
            return (false, null);
        }
    }
}
