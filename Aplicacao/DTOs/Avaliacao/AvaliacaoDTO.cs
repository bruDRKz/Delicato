namespace DelicatoProject.Aplicacao.DTOs.Avaliacao
{
    public class AvaliacaoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;

        public Models.Avaliacao ConvertToEntity()
        {
            return new Models.Avaliacao
            {
                Nome = this.Nome,
                Nota = this.Nota,
                Comentario = this.Comentario ?? "—",
                DataAvaliacao = this.DataAvaliacao
            };
        }

    }
}
