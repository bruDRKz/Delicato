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

        
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int Nota { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Comentario { get; set; } = string.Empty;

        public DateTime DataAvaliacao { get; set; } = DateTime.Now;

        public Avaliacao() { }

        public Avaliacao(string nome, int nota, string comentario)
        {
            Nome = nome;
            Nota = nota;
            Comentario = comentario;
            DataAvaliacao = DateTime.Now;
        }
    }
}

