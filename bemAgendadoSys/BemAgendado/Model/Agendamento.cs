using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemAgendado.Model
{
    public class Agendamento
    {
        public int id { get; set; }
        public string nome_paciente { get; set; }
        public DateTime data_agendamento { get; set; }
        public TimeSpan horario_agendamento { get; set; }
        public string email { get; set; }
        public string status { get; set; }
    }
}
