using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Infraestrutura;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacienteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/paciente
        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] CreatePacienteRequest request)
        {
            try
            {
                Console.WriteLine($"=== TENTANDO CRIAR PACIENTE: {request.nome} ===");

                // Validação manual dos campos obrigatórios
                if (string.IsNullOrEmpty(request.nome))
                    return BadRequest(new { sucesso = false, mensagem = "Nome é obrigatório" });

                if (string.IsNullOrEmpty(request.cpf))
                    return BadRequest(new { sucesso = false, mensagem = "CPF é obrigatório" });

                if (string.IsNullOrEmpty(request.telefone))
                    return BadRequest(new { sucesso = false, mensagem = "Telefone é obrigatório" });

                if (string.IsNullOrEmpty(request.endereco))
                    return BadRequest(new { sucesso = false, mensagem = "Endereço é obrigatório" });

                if (string.IsNullOrEmpty(request.datanascimento))
                    return BadRequest(new { sucesso = false, mensagem = "Data de nascimento é obrigatória" });

                if (string.IsNullOrEmpty(request.genero))
                    return BadRequest(new { sucesso = false, mensagem = "Gênero é obrigatório" });

                // CONVERSÃO DA DATA
                DateTime dataNascimento;
                try
                {
                    // Tenta converter do formato brasileiro (dd/MM/yyyy)
                    if (request.datanascimento!.Contains('/'))
                    {
                        dataNascimento = DateTime.ParseExact(request.datanascimento, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    // Tenta converter do formato ISO (yyyy-MM-dd)
                    else
                    {
                        dataNascimento = DateTime.Parse(request.datanascimento);
                    }
                }
                catch (FormatException)
                {
                    return BadRequest(new
                    {
                        sucesso = false,
                        mensagem = "Formato de data inválido. Use dd/MM/yyyy (ex: 15/01/1990) ou yyyy-MM-dd (ex: 1990-01-15)"
                    });
                }

                // Verifica se já existe um paciente com o mesmo email 
                if (!string.IsNullOrEmpty(request.email))
                {
                    var pacienteExistente = await _context.paciente
                        .FirstOrDefaultAsync(p => p.email == request.email);

                    if (pacienteExistente != null)
                    {
                        return BadRequest(new
                        {
                            sucesso = false,
                            mensagem = "Já existe um paciente cadastrado com este email"
                        });
                    }
                }

                // Verifica se já existe um paciente com o mesmo CPF
                var cpfExistente = await _context.paciente
                    .FirstOrDefaultAsync(p => p.cpf == request.cpf);

                if (cpfExistente != null)
                {
                    return BadRequest(new
                    {
                        sucesso = false,
                        mensagem = "Já existe um paciente cadastrado com este CPF"
                    });
                }

                // Cria novo paciente COM DATA CONVERTIDA
                var novoPaciente = new PacienteModels
                {
                    nome = request.nome!.Trim(),
                    email = request.email?.Trim(),
                    senha = request.senha?.Trim(),
                    cpf = request.cpf!.Trim(),
                    telefone = request.telefone!.Trim(),
                    endereco = request.endereco!.Trim(),
                    datanascimento = dataNascimento, // Agora é DateTime
                    genero = request.genero!.Trim(),
                    status = "Ativo"
                };

                Console.WriteLine($"Salvando paciente: {novoPaciente.nome}, Data: {novoPaciente.datanascimento}");

                // Adiciona ao contexto
                _context.paciente.Add(novoPaciente);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Paciente criado com ID: {novoPaciente.id}");

                return Ok(new
                {
                    sucesso = true,
                    mensagem = "Paciente criado com sucesso",
                    paciente = new
                    {
                        id = novoPaciente.id,
                        nome = novoPaciente.nome,
                        cpf = novoPaciente.cpf,
                        email = novoPaciente.email,
                        telefone = novoPaciente.telefone,
                        status = novoPaciente.status,
                        datanascimento = novoPaciente.datanascimento?.ToString("dd/MM/yyyy"), 
                    }
                });
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"ERRO BANCO: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return StatusCode(500, new
                {
                    sucesso = false,
                    mensagem = "Erro ao salvar no banco de dados",
                    erro = dbEx.InnerException?.Message
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO GERAL: {ex.Message}");
                return StatusCode(500, new
                {
                    sucesso = false,
                    mensagem = $"Erro interno: {ex.Message}"
                });
            }
        }

        // POST: api/paciente/verificar
        [HttpPost("verificar")]
        public async Task<IActionResult> VerificarPaciente([FromBody] VerificacaoPacienteRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.email))
                    return BadRequest(new { existe = false, mensagem = "Email é obrigatório" });

                var paciente = await _context.paciente
                    .FirstOrDefaultAsync(p => p.email == request.email);

                if (paciente == null)
                {
                    return Ok(new
                    {
                        existe = false,
                        mensagem = "Paciente não encontrado"
                    });
                }

                if (!string.IsNullOrEmpty(request.senha) && paciente.senha != request.senha)
                {
                    return Ok(new
                    {
                        existe = false,
                        mensagem = "Senha incorreta"
                    });
                }

                return Ok(new
                {
                    existe = true,
                    mensagem = "Paciente encontrado",
                    
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    existe = false,
                    mensagem = $"Erro interno: {ex.Message}"
                });
            }
        }

        // GET: api/paciente/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> VerificarPorEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return BadRequest(new { existe = false, mensagem = "Email é obrigatório" });

                var paciente = await _context.paciente
                    .FirstOrDefaultAsync(p => p.email == email);

                if (paciente == null)
                {
                    return Ok(new
                    {
                        existe = false,
                        mensagem = "Paciente não encontrado"
                    });
                }

                return Ok(new
                {
                    existe = true,
                    mensagem = "Paciente encontrado",
                    paciente = new
                    {
                        id = paciente.id,
                        nome = paciente.nome,
                        email = paciente.email,
                        telefone = paciente.telefone,
                        status = paciente.status
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    existe = false,
                    mensagem = $"Erro interno: {ex.Message}"
                });
            }
        }
    }

    // Model para criar paciente
    public class CreatePacienteRequest
    {
        public string? nome { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public string? cpf { get; set; }
        public string? telefone { get; set; }
        public string? endereco { get; set; }
        public string? datanascimento { get; set; }
        public string? genero { get; set; }
    }

    // Model para verificar paciente
    public class VerificacaoPacienteRequest
    {
        public string? email { get; set; }
        public string? senha { get; set; }
    }
}