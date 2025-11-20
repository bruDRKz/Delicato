namespace DelicatoProject.Aplicacao.DTOs.Cardapio
{
    public class CardapioBebidaDTO
    {
        public int IdBebida { get; set; }
        public string NomeBebida { get; set; } = string.Empty;
        public string DescricaoBebida { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int IdCategoria { get; set; }
        public string? ImagemUrl { get; set; } = string.Empty;
        public bool Disponivel { get; set; }
    }
}
