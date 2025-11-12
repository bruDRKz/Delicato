using DelicatoProject.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DelicatoProject.Controllers
{
    public class UsuarioController : Controller
    {
        //Usar isso no momento de criar o usuario
        private readonly IUsuarioService usuarioService;
        public UsuarioController()
        {
            //criar uma instancia do usuario service
        }
        [HttpPost]
        public JsonResult AdicionarUsuario([FromBody] Models.Usuario usuario)
        {
            usuarioService.AdicionarUsuario(usuario);
            return Json(new { sucesso = true, mensagem = "Usuário adicionado com sucesso!" });
        }
    }
}
