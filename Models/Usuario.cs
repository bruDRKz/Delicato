using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace DelicatoProject.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string SenhaHash { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        
        public List<Reservas> Reservas { get; set; } = new List<Reservas>();
        
        public Usuario() { }
        public Usuario(string nome, string senha, string telefone = "")
        {
            Nome = nome;            
            SenhaHash = SenhaEncripter(senha);
            Telefone = telefone;
        }

        //Retorna true se a senha for igual a senha encriptada — Deixei publico porque vou usar no Repository
        public bool VerificarSenha(string senhaParaVerificar)
        {
            var senhaEncriptada = SenhaEncripter(senhaParaVerificar);
            return SenhaHash == senhaEncriptada;
        }
        private string SenhaEncripter(string senhaOriginal)
        {
            senhaOriginal = senhaOriginal.Trim().Normalize();
            var chaveAdicional = "DelicatoProject";
            var newPassword = $"{senhaOriginal}{chaveAdicional}";
            var bytes = Encoding.UTF8.GetBytes(newPassword);

            //encripta a senha utilizando SHA-512, depois retorna a string em hexadecimal — A senha é encriptada, não criptografada
            var hashBytes = SHA512.HashData(bytes);
            return StringBytes(hashBytes);
        }
        

        //Converte o array de bytes para string hexadecimal
        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return sb.ToString();
        }        
    }
}
