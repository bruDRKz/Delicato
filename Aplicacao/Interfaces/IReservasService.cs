using DelicatoProject.Models;

namespace DelicatoProject.Aplicacao.Interfaces
{
    public interface IReservasService
    {
        public Task<(bool Sucesso, string Mensagem)> CriarNovaReserva(Reservas reservas);        
        public Task<List<Reservas>> ObterReservasPorUsuario(int idUsuario);         
        public Task<List<Reservas>> ObterReservasPorPeriodo(DateTime dataInicio, DateTime dataFim);
        public Task<(bool Sucesso, string Mensagem)> DeletarReserva(int idReserva);
        public Task<(bool Sucesso, string Mensagem)> ConcluirReserva(int idReserva);
        public Task<(bool Sucesso, string Mensagem)> BloquearData(DateTime data, string Motivo);

    }
}
