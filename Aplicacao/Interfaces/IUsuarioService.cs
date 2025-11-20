using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Interfaces
{
    public interface IUsuarioService
    {
        public void AdicionarUsuario(Usuario usuario);
        public (bool Existe, int? Id) JaExisteUsuarioComTelefone(string telefone);
        public (bool Autenticado, int? Id) AutenticarUsuario(string telefone, string senha);
    }
}
