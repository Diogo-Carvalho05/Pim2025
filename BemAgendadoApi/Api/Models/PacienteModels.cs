using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class PacienteModels
    {
        public int id { get; set; }

        public string? nome { get; set; }
        public string? telefone { get; set; }
        public string? endereco { get; set; }
        public DateTime ? datanascimento { get; set; }
        public string? genero { get; set; }
        public string? cpf { get; set; }
        public string? status { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
    }
}
