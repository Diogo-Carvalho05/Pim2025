using Api.Infraestrutura;
using Api.Model;
using Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Agendamentos
        [HttpPost]
        public async Task<ActionResult<AgendamentoModels>> PostAgendamento([FromBody] AgendamentoCreateDto dto)
        {
            try
            {
                var agendamento = new AgendamentoModels
                {
                    nomepaciente = dto.nomepaciente,
                    pacientes_id = dto.pacientes_id,
                    email = dto.Email,
                    data_agendamento = dto.data_agendamento,
                    horario_agendamento = dto.horario_agendamento
                };

                _context.agenda.Add(agendamento);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAgendamentoById), new { id = agendamento.id }, agendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Erro ao criar agendamento" ,
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        // GET por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendamentoModels>> GetAgendamentoById(int id)
        {
            var agendamento = await _context.agenda.FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento;
        }
    }
}
