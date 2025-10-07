namespace Api.Model
{
    public enum status_agendamento
    {
        pendente,
        confirmado,
        cancelado
    }

    public class AgendamentoModels
    {
        public int id { get; set; }
        public int pacientes_id { get; set; }
        public required string nomepaciente { get; set; }
        public required string email { get; set; }
        public required DateTime data_agendamento { get; set; }
        public required TimeSpan horario_agendamento { get; set; }
  
    }
}
