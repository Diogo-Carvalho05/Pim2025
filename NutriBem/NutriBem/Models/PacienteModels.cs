using System.ComponentModel.DataAnnotations;

namespace NutriBem.Models
{
    public class PacienteModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string? nome { get; set; }
        public string? cpf { get; set; }

        public string? telefone { get; set; }
        public string? endereco { get; set; }
        public DateTime? datanascimento { get; set; }
        public string? genero { get; set; }
        public string? status { get; set; }
        public DateTime? datacriacao { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string? email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string? senha { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }
}