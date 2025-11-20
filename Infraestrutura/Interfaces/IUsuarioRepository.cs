using DelicatoProject.Models;

namespace DelicatoProject.Infraestrutura.Interfaces
{
    public interface IUsuarioRepository
    {
        public void AdicionarUsuario(Usuario usuario);
        public (bool Existe, int? Id) JaExisteUsuarioComTelefone(string telefone);
        public (bool Autenticado, int? Id) AutenticarUsuario(string telefone, string senha);
    }
}
