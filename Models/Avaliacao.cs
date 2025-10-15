using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DelicatoProject.Models
{
    [Table("Avaliacao")]
    public class Avaliacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAvaliacao { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }  //FK da tabela Usuario
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   

        [MaxLength(1000)]
        public string Comentario { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null!; // Propriedade de navegação — Para o relacionamento com a tabela Usuario

        public Avaliacao() {}

        public Avaliacao(int idUsuario, string nome, string email, string comentario)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Comentario = comentario;
        }
    }
}
