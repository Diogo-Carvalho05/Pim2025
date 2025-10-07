using BemAgendado.Model;
using System;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BemAgendado.Controler
{
    public class EmailService
    {
        private readonly BuscarAgenda buscarAgenda;

        public EmailService(BuscarAgenda buscarAgenda)
        {
            this.buscarAgenda = buscarAgenda;
        }

        public async Task<bool> TestarConexaoAPI()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7172/");
                    var response = await client.GetAsync("api/notificacoes");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }


        public async Task EnviarEmailConfirmacaoAPI(int agendamentoId)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:7172/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                    );

                    var agendamento = buscarAgenda.ObterAgendamentoPorId(agendamentoId);

                    if (agendamento == null)
                    {
                        MessageBox.Show("Agendamento não encontrado no banco.");
                        return;
                    }

                    var dto = new
                    {
                        email = agendamento.email,
                        nomepaciente = agendamento.nome_paciente,
                        data_agendamento = agendamento.data_agendamento,
                        horario_agendamento = agendamento.horario_agendamento
                    };

                    var response = await client.PostAsJsonAsync("api/Notificacoes/enviar-confirmacao", dto);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("E-mail de confirmação enviado ao paciente!",
                                        "Notificação",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao enviar e-mail de confirmação: {errorContent}",
                                        "Erro",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao chamar API: {ex.Message}",
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }



        public async Task EnviarEmailCancelamentoAPI(int agendamentoId)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:7172/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                    );

                    var agendamento = buscarAgenda.ObterAgendamentoPorId(agendamentoId);

                    if (agendamento == null)
                    {
                        MessageBox.Show("Agendamento não encontrado no banco.");
                        return;
                    }

                    var dto = new
                    {
                        email = agendamento.email,
                        nomepaciente = agendamento.nome_paciente,
                        data_agendamento = agendamento.data_agendamento,
                        horario_agendamento = agendamento.horario_agendamento
                    };

                    var response = await client.PostAsJsonAsync("api/Notificacoes/enviar-cancelamento", dto);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("E-mail de cancelamento enviado ao paciente!",
                                        "Notificação",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao enviar e-mail de cancelamento: {errorContent}",
                                        "Erro",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao chamar API: {ex.Message}",
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
