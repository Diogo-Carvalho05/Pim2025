using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs
{
    public class AgendamentoCreateDto
    {
        public int pacientes_id { get; set; }
        public string nomepaciente { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime data_agendamento { get; set; }
        public TimeSpan horario_agendamento { get; set; }
    }
}
