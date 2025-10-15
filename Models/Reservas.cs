using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DelicatoProject.Models
{
    [Table("Reservas")]
    public class Reservas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReserva { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }  //FK da tabela Usuario
        public string NomeNaReserva { get; set; } = string.Empty;
        public string DataReserva { get; set; } = string.Empty;
        public string HoraReserva { get; set; } = string.Empty;
        public int NumeroPessoas { get; set; }
        public string Contato { get; set; } = string.Empty;
        public bool ReservaConcluida { get; set; } = false;

        [MaxLength(500)]
        public string Observacoes { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null!; // Propriedade de navegação — Para o relacionamento com a tabela Usuario

        public Reservas() { }
    }
}
