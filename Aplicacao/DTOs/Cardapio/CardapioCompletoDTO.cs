namespace DelicatoProject.Aplicacao.DTOs.Cardapio
{
    public class CardapioCompletoDTO
    {
        public List<CardapioBebidaDTO> BebidasDTO { get; set; } = new();
        public List<CardapioComidaDTO> ComidasDTO { get; set; } = new();
    }
}
