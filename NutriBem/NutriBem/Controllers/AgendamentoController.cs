using Microsoft.AspNetCore.Mvc;
using NutriBem.Models;
using System.Text;
using System.Text.Json;

namespace NutriBem.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly HttpClient _httpClient;

        public AgendamentoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5254/api/");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public IActionResult Index()
        {
            var nome = HttpContext.Session.GetString("UsuarioNome");
            ViewBag.NomePaciente = nome ?? "Paciente";

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AgendamentoCreateDto model)
        {
            var pacienteId = HttpContext.Session.GetInt32("UsuarioId");
            var pacienteEmail = HttpContext.Session.GetString("UsuarioEmail");
            var pacienteNome = HttpContext.Session.GetString("UsuarioNome");

            if (pacienteId == null || string.IsNullOrEmpty(pacienteEmail) || string.IsNullOrEmpty(pacienteNome))
            {
                TempData["MensagemErro"] = "Informações do paciente não encontradas. Faça login novamente.";
                return RedirectToAction("Index", "Login");
            }

            // Preenche os dados automaticamente
            model.pacientes_id = pacienteId.Value;
            model.Email = pacienteEmail;
            model.nomepaciente = pacienteNome;

            try
            {
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("Agendamentos", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["MensagemSucesso"] = "Agendamento realizado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = $"Erro ao agendar: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro de conexão: {ex.Message}";
            }

            return View(model);
        }


    }
}
