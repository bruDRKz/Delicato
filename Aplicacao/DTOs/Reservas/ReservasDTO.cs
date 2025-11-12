namespace DelicatoProject.Aplicacao.DTOs.Reservas
{
    public class ReservasDTO
    {
        public int IdUsuario { get; set; }
        public string NomeNaReserva { get; set; } = string.Empty;
        public string DataReserva { get; set; } = string.Empty; 
        public string HoraReserva { get; set; } = string.Empty;
        public int NumeroPessoas { get; set; }
        public string ContatoReserva { get; set; } = string.Empty;  
        public string Observacoes { get; set; } = string.Empty;

        //Converte DTO para a entidade de Reservas, porque meus metodos de serviço trabalham com a entidade
        public Models.Reservas ToEntity()
        { 
            return new Models.Reservas
            {
                IdUsuario = this.IdUsuario,
                NomeNaReserva = this.NomeNaReserva,
                DataReserva = this.DataReserva,
                HoraReserva = this.HoraReserva,
                NumeroPessoas = this.NumeroPessoas,
                Contato = this.ContatoReserva,
                Observacoes = this.Observacoes
            };
        }

    }
}
