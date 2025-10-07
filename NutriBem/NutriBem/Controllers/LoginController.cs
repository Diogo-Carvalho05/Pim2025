using Microsoft.AspNetCore.Mvc;
using NutriBem.Models;
using System.Text;
using System.Text.Json;

namespace NutriBem.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5254/api/");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        // TELA DE LOGIN
        public IActionResult Index()
        {
            // Se já está logado, redireciona para Home
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioEmail")))
            {
                return RedirectToAction("Index", "Agendamento");
            }
            return View();
        }

        // PROCESSAR LOGIN
        [HttpPost]
        public async Task<IActionResult> Index(PacienteModels model)
        {

            if (string.IsNullOrEmpty(model.email) || string.IsNullOrEmpty(model.senha))
            {
                TempData["MensagemErro"] = "Preencha email e senha";
                return View(model);
            }

            try
            {
                var loginData = new { email = model.email, senha = model.senha };
                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("paciente/verificar", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Resposta API: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<VerificacaoResponse>(responseContent);

                    if (result?.existe == true)
                    {
                        HttpContext.Session.SetString("UsuarioEmail", model.email);
                        HttpContext.Session.SetString("UsuarioNome", "Usuário"); // Temporário

                        // ✅ CHAMADA PARA PEGAR DADOS DO PACIENTE
                        var responsePaciente = await _httpClient.GetAsync($"paciente/{model.email}");
                        var pacienteContent = await responsePaciente.Content.ReadAsStringAsync();

                        if (responsePaciente.IsSuccessStatusCode)
                        {
                            var pacienteData = JsonSerializer.Deserialize<VerificacaoResponse>(pacienteContent);

                            if (pacienteData?.paciente != null)
                            {
                                HttpContext.Session.SetInt32("UsuarioId", pacienteData.paciente.id);
                                HttpContext.Session.SetString("UsuarioNome", pacienteData.paciente.nome ?? "Usuário");
                            }
                        }

                        return Redirect("/Agendamento");
                    }

                }

                TempData["MensagemErro"] = "Email ou senha incorretos";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                TempData["MensagemErro"] = $"Erro de conexão: {ex.Message}";
            }

            return View(model);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["MensagemSucesso"] = "Logout realizado com sucesso!";
            return RedirectToAction("Index");
        }

        // POST: /Login/Cadastrar
        [HttpPost]
        public async Task<IActionResult> Cadastrar(PacienteModels model)
        {
            try
            {
                // Dados para a API
                var cadastroData = new
                {
                    nome = model.nome,
                    email = model.email,
                    senha = model.senha,
                    cpf = model.cpf,
                    telefone = model.telefone,
                    endereco = model.endereco,
                    datanascimento = model.datanascimento?.ToString("dd/MM/yyyy"),
                    genero = model.genero
                };

                var json = JsonSerializer.Serialize(cadastroData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Chamada para API
                var response = await _httpClient.PostAsync("paciente", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["MensagemSucesso"] = "Cadastro realizado com sucesso! Faça login.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    TempData["MensagemErro"] = $"Erro no cadastro: {erro}";
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro: {ex.Message}";
            }

            return View(model);
        }


    }

    // MODEL PARA RESPOSTA DA API
    public class VerificacaoResponse
    {
        public bool existe { get; set; }
        public string? mensagem { get; set; }
        public PacienteData? paciente { get; set; }
    }

    public class PacienteData
    {
        public int id { get; set; }
        public string? nome { get; set; }
        public string? email { get; set; }
        public string? telefone { get; set; }
        public string? status { get; set; }
    }
}