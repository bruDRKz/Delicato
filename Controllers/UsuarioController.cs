using DelicatoProject.Aplicacao.Interfaces;
using DelicatoProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DelicatoProject.Controllers
{
    public class UsuarioController : Controller
    {
        //Usar isso no momento de criar o usuario
        private readonly IUsuarioService usuarioService;
        public UsuarioController(IUsuarioService _usuarioService)
        {
            usuarioService = _usuarioService;
        }
        [HttpPost]
        public JsonResult AdicionarUsuario(string nome, string senha, string telefone)
        {
            try
            {
                Usuario usuario = new Usuario(nome, senha, telefone);
                usuarioService.AdicionarUsuario(usuario);
                return Json(new { sucesso = true, ID = usuario.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public JsonResult VerificarExistenciaUsuario(string telefone)
        {
            //Vou verificar se o usuario ja existe, porque inicialmente não havia sido pensado em simular contas para o controle de reservas
            //Essa foi a melhor maneira que achei para adaptar de acordo com o que ja existia no BD sem causar muitos conflitos
            //Caso o usuario ja exista, retornarei o ID do usuario existente, mesmo fluxo de adicionar novo usuario
            try
            {
                var usuarioExistente = usuarioService.JaExisteUsuarioComTelefone(telefone);
                return Json(new { Existe = usuarioExistente.Existe, ID = usuarioExistente.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public JsonResult AutenticarUsuario(string telefone, string senha)
        {
            try
            {
                var autenticacao = usuarioService.AutenticarUsuario(telefone.Trim(), senha);
                return Json(new { sucesso = autenticacao.Autenticado, ID = autenticacao.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
