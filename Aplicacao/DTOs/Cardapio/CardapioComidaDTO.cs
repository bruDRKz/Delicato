namespace DelicatoProject.Aplicacao.DTOs.Cardapio
{
    public class CardapioComidaDTO
    {
        public int IdComida { get; set; }
        public string NomeComida { get; set; } = string.Empty;
        public string DescricaoComida { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int IdCategoria { get; set; }
        public string? ImagemUrl { get; set; } = string.Empty;
        public bool Disponivel { get; set; }
    }
}
