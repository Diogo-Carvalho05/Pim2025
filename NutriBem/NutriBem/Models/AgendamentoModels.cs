using System;
using System.ComponentModel.DataAnnotations;

namespace NutriBem.Models
{
    public class AgendamentoCreateDto
    {
        public string nomepaciente { get; set; }
        public int pacientes_id { get; set; }
        public string Email { get; set; }
        public string data_agendamento { get; set; }
        public string horario_agendamento { get; set; }
    }


}
