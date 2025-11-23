using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DelicatoProject.Models
{
    [Table("ReservasBloqueio")]
    public class ReservasBloqueio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReservaBloqueio { get; set; }
        public string DataBloqueio { get; set; } = string.Empty;

        [MaxLength(250)]
        public string MotivoBloqueio { get; set; } = string.Empty;
        public string CriadoEm { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true; 
    }
}
