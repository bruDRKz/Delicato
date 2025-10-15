using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DelicatoProject.Models
{
    [Table("Categorias")]
    public class Categorias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public ICollection<CardapioBebidas> cardapioBebidas { get; set; } = null!;
        public ICollection<CardapioComidas> cardapioComidas { get; set; } = null!;

        public Categorias() { }

    }
}
