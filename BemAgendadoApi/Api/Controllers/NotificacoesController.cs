using Microsoft.AspNetCore.Mvc;
using Api.Infraestrutura;
using Api.Model;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacoesController : ControllerBase
    {
        private readonly EmailService _emailService;

        public NotificacoesController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("enviar-confirmacao")]
        public async Task<IActionResult> EnviarConfirmacao([FromBody] AgendamentoModels dto)
        {
           

            // ✅ Formatação de data e hora
            var dataFormatada = dto.data_agendamento.ToString("dd/MM/yyyy");
            var horaFormatada = dto.horario_agendamento.ToString(@"hh\:mm");

            var assunto = "Confirmação de Agendamento";

            var corpo = $@"
                <h2>Olá {dto.nomepaciente},</h2>
                <p>Seu agendamento foi <b>confirmado</b> com sucesso!</p>
                <p><b>Data:</b> {dataFormatada}</p> 
                <p><b>Horário:</b> {horaFormatada}</p>
                <br/>
                <p>Atenciosamente,<br/>Equipe Bem Agendado</p>
            ";

            await _emailService.SendEmailAsync(dto.email, assunto, corpo);

            return Ok("E-mail de confirmação enviado!");
        }

        [HttpPost("enviar-cancelamento")]
        public async Task<IActionResult> EnviarCancelamento([FromBody] AgendamentoModels dto)
        {

            // ✅ Formatação de data e hora
            var dataFormatada = dto.data_agendamento.ToString("dd/MM/yyyy");
            var horaFormatada = dto.horario_agendamento.ToString(@"hh\:mm");

            var assunto = "Agendamento Cancelado";

            var corpo = $@"
                <h2>Olá {dto.nomepaciente},</h2>
                <p>Seu agendamento da data e hora abaixo foi <b>Cancelado!</b></p>
                <p><b>Data:</b> {dataFormatada}</p> 
                <p><b>Horário:</b> {horaFormatada}</p>
                <br/>
                <p>Por gentileza entrar em contato com a<br/>Equipe Bem Agendado</p>
            ";

            await _emailService.SendEmailAsync(dto.email, assunto, corpo);

            return Ok("E-mail de cancelamento enviado!");
        }
    }
}
