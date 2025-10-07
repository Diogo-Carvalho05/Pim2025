using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemAgendado.Model
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string email { get; set; }
        private  string senha{ get; set; }

    }
}
