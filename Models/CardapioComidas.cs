using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DelicatoProject.Models
{
    [Table("CardapioComidas")]
    public class CardapioComidas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComida { get; set; }

        [Required]
        public string NomeComida { get; set; } = string.Empty;

        public string DescricaoComida { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; } // FK correta

        public string ImagemUrl { get; set; } = string.Empty;

        public bool Disponivel { get; set; } = true;

        public Categorias Categoria { get; set; } = null!; // Navegação

        public CardapioComidas() { }
    }
}
