using BemAgendado.Controler;
using BemAgendado.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace BemAgendado.Views
{


    public partial class Menu : Form
    {
        private MonthCalendar monthCalendar;
        private bool calendarioVisivel = false;
        private DateTimePicker timePicker;
        private bool timePickerVisivel = false;
        private Button btTrocaData;
        private Button btTrocarHora;
        private int agendamentoIdSelecionado;
        private System.Windows.Forms.Timer timerAtualizacao;
        private bool apenasFuturos = false;



        // Botões de confirmação e cancelamento
        private Button btnConfirmarData;
        private Button btnCancelarData;
        private Button btnConfirmarHora;
        private Button btnCancelarHora;


        // Variáveis para armazenar seleções temporárias
        private DateTime dataSelecionadaTemp;
        private TimeSpan horaSelecionadaTemp;

        public Menu()
        {
            InitializeComponent();

            InitializeUsuarioInfo();

            InitializeCalendar();
            InitializeTimePicker();

            ConfigurarBotoesExistentes();

            InitializeDataGridView();


            apenasFuturos = ConfiguracoesApp.FiltrarAgendamentosFuturos;

            CarregarDados(apenasFuturos);
            AtualizarEstadoBotoes();
        }

        private void InitializeUsuarioInfo()
        {
            // Verifica se há usuário logado
            if (!UsuarioSessao.EstaLogado)
            {
                MessageBox.Show("Nenhum usuário logado. Redirecionando para login.");
                this.Close();

                // Reabre o formulário de login
                var login = new Login();
                login.Show();
                return;
            }
            else
            {
                // Exibe as informações do usuário logado
                LblUsuarioLogado.Text = $"Usuário: {UsuarioSessao.UsuarioLogado.NomeUsuario}";

            }
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            VerificarERecriarColunasSeNecessario();

            timerAtualizacao = new System.Windows.Forms.Timer();
            timerAtualizacao.Interval = 180000; // 30 minutos = 180 segundos = 180000 ms
            timerAtualizacao.Tick += TimerAtualizacao_Tick;
            timerAtualizacao.Start();

            CarregarDadosIniciais();
        }

        private void ConfigurarBotoesExistentes()
        {
            // Associa os eventos de clique aos botões existentes
            btnTodosAgendamentos.Click += (sender, e) => AlternarFiltroFuturos(false);
            btnProximosAgendamentos.Click += (sender, e) => AlternarFiltroFuturos(true);
        }
        private void InitializeDataGridView()
        {
            agendamento.Size = new Size(700, 300);
            int centerX = (this.ClientSize.Width - agendamento.Width) / 2;
            agendamento.Location = new Point(centerX, 150);
            agendamento.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;

            // ✅ LIMPAR COLUNAS EXISTENTES PRIMEIRO
            agendamento.Columns.Clear();

            // ✅ CONFIGURAR COLUNAS NA ORDEM CORRETA
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "id";
            colId.Name = "ID";
            colId.Visible = false;
            agendamento.Columns.Add(colId);

            DataGridViewTextBoxColumn colData = new DataGridViewTextBoxColumn();
            colData.HeaderText = "Data de Agendamento";
            colData.Name = "DataAgendamento";
            colData.Width = 150;
            agendamento.Columns.Add(colData);

            DataGridViewTextBoxColumn colNome = new DataGridViewTextBoxColumn();
            colNome.HeaderText = "Nome do Paciente";
            colNome.Name = "NomePaciente";
            colNome.Width = 200;
            agendamento.Columns.Add(colNome);

            DataGridViewTextBoxColumn colEmail = new DataGridViewTextBoxColumn();
            colEmail.HeaderText = "Email";
            colEmail.Name = "Email";
            colEmail.Width = 200;
            agendamento.Columns.Add(colEmail);

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.HeaderText = "Status";
            colStatus.Name = "Status";
            colStatus.Width = 100;
            agendamento.Columns.Add(colStatus);

            // Configurações de aparência e comportamento
            agendamento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            agendamento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            agendamento.MultiSelect = false;
            agendamento.ReadOnly = true;
            agendamento.AllowUserToAddRows = false;
            agendamento.AllowUserToDeleteRows = false;

            EstilizarCabecalhosColunas();

            // Impede seleção de células individuais (só permite seleção de linha inteira)
            agendamento.CellMouseClick += (sender, e) =>
            {
                if (e.RowIndex == -1) // Clicou no cabeçalho
                {
                    agendamento.ClearSelection();
                }
            };

            agendamento.CellFormatting += agendamento_CellFormatting;
            agendamento.ClearSelection();

            agendamento.ColumnHeaderMouseClick += (sender, e) =>
            {
                agendamento.ClearSelection();
            };

            agendamento.SelectionChanged += agendamento_SelectionChanged;

            this.Controls.Add(agendamento);
            this.Size = new Size(750, 500);
            this.SizeChanged += Menu_SizeChanged;
        }
        private void agendamento_SelectionChanged(object sender, EventArgs e)
        {
            if (agendamento.CurrentRow != null && agendamento.CurrentRow.Index >= 0)
            {
                var valor = agendamento.CurrentRow.Cells[0].Value;
                if (valor != null)
                {
                    agendamentoIdSelecionado = Convert.ToInt32(valor);
                } 
            }
        }
        private void agendamento_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && agendamento.Columns[e.ColumnIndex].Name == "Status")
            {
                string status = agendamento.Rows[e.RowIndex].Cells["Status"].Value?.ToString();

                if (!string.IsNullOrEmpty(status))
                {
                    switch (status.ToLower())
                    {
                        case "confirmado":
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        case "cancelado":
                            e.CellStyle.BackColor = Color.LightCoral;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        case "pendente":
                            e.CellStyle.BackColor = Color.Orange;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                    }
                }
            }
        }
        private void EstilizarCabecalhosColunas()
        {
           

            agendamento.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 72, 124);
            agendamento.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            agendamento.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            agendamento.EnableHeadersVisualStyles = false;             
            agendamento.ColumnHeadersHeight = 35;

            agendamento.ColumnHeadersDefaultCellStyle.SelectionBackColor = agendamento.ColumnHeadersDefaultCellStyle.BackColor;
            agendamento.ColumnHeadersDefaultCellStyle.SelectionForeColor = agendamento.ColumnHeadersDefaultCellStyle.ForeColor;
        }
        private void CarregarDados(bool filtrarFuturos = false)
        {
            try
            {
                VerificarERecriarColunasSeNecessario(); // ✅ CHAMADA DE SEGURANÇA

                agendamento.Rows.Clear();


                using (var db = new DbBemAgendado())
                using (var buscarAgenda = new BuscarAgenda(db))
                {
                    var listaAgendamentos = buscarAgenda.BuscarTodosAgendamentos();

                    // Filtrar agendamentos futuros se solicitado
                    if (filtrarFuturos)
                    {
                        listaAgendamentos = FiltrarAgendamentosFuturos(listaAgendamentos);
                    }

                    foreach (var ag in listaAgendamentos)
                    {
                        // ✅ VERIFICAR SE AS COLUNAS EXISTEM ANTES DE ADICIONAR
                        if (agendamento.Columns.Count >= 5)
                        {
                            agendamento.Rows.Add(
                                ag.id,
                                $"{ag.data_agendamento:dd/MM/yyyy} {ag.horario_agendamento:hh\\:mm}",
                                ag.nome_paciente,
                                ag.email,
                                ag.status
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar agendamentos: " + ex.Message);
            }
        }
        private void Menu_SizeChanged(object sender, EventArgs e)
        {
            CentralizarDataGridView();
        }
        private void CentralizarDataGridView()
        {
            if (agendamento != null)
            {
                int centerX = (this.ClientSize.Width - agendamento.Width) / 2;
                agendamento.Location = new Point(
                    centerX,
                    agendamento.Location.Y
                );
            }
        }
        private void agendamento_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = agendamento.Rows[e.RowIndex];
                if (row.Cells["ID"].Value != null)
                {
                    agendamentoIdSelecionado = Convert.ToInt32(agendamento.Rows[e.RowIndex].Cells[0].Value);
                }
            }
        }
        private List<Agendamento> FiltrarAgendamentosFuturos(List<Agendamento> agendamentos)
        {
            DateTime agora = DateTime.Now;

            return agendamentos.Where(ag =>
            {
                // Combina data e horário para comparação precisa
                DateTime dataHoraAgendamento = ag.data_agendamento.Date + ag.horario_agendamento;
                return dataHoraAgendamento > agora;
            }).ToList();
        }
        private void AlternarFiltroFuturos(bool mostrarApenasFuturos)
        {
            apenasFuturos = mostrarApenasFuturos;

            // Salva a preferência na variável estática
            ConfiguracoesApp.FiltrarAgendamentosFuturos = apenasFuturos;

            // Para o timer temporariamente para evitar loop
            timerAtualizacao.Stop();

            CarregarDados(apenasFuturos);
            AtualizarEstadoBotoes();

            // Reinicia o timer após 1 segundo
            Timer reiniciarTimer = new Timer();
            reiniciarTimer.Interval = 1000;
            reiniciarTimer.Tick += (s, e) => {
                timerAtualizacao.Start();
                reiniciarTimer.Stop();
            };
            reiniciarTimer.Start();
        }
        private void CarregarDadosIniciais()
        {
            CarregarDados(false);
        }
        private void AtualizarEstadoBotoes()
        {
            if (btnTodosAgendamentos != null && btnProximosAgendamentos != null)
            {
                // Destaca o botão selecionado
                btnTodosAgendamentos.BackColor = apenasFuturos ? SystemColors.Control : Color.LightBlue;
                btnProximosAgendamentos.BackColor = apenasFuturos ? Color.LightBlue : SystemColors.Control;

                btnTodosAgendamentos.Font = new Font("Arial", 9, apenasFuturos ? FontStyle.Regular : FontStyle.Bold);
                btnProximosAgendamentos.Font = new Font("Arial", 9, apenasFuturos ? FontStyle.Bold : FontStyle.Regular);
            }
        }
        public static class ConfiguracoesApp
        {
            public static bool FiltrarAgendamentosFuturos { get; set; } = false;
        }

        private void VerificarERecriarColunasSeNecessario()
        {
            if (agendamento.Columns.Count == 0)
            {
                // Recria as colunas
                InitializeDataGridView();
            }
        }




        //configuarar calendario
        private void InitializeCalendar()
        {
            monthCalendar = new MonthCalendar();
            monthCalendar.Size = new Size(200, 160);
            monthCalendar.Visible = false;
            monthCalendar.DateSelected += MonthCalendar_DateSelected;
            this.Controls.Add(monthCalendar);
        }
        private void BtnConfirmarData_Click(object sender, EventArgs e)
        {
            // Confirma a seleção da data
            DateTime dataConfirmada = monthCalendar.SelectionStart;
            MessageBox.Show($"Data confirmada: {dataConfirmada:dd/MM/yyyy}");

            // Chama o método para alterar a data no banco
            using (var buscarAgenda = new BuscarAgenda(new DbBemAgendado()))
            {
                bool sucesso = buscarAgenda.AlterarData(agendamentoIdSelecionado, dataConfirmada);
                if (sucesso)
                    MessageBox.Show("Data alterada com sucesso!");
                else
                    MessageBox.Show("Falha ao alterar a data.");
            }
            EsconderCalendario();
        }
        private void BtnCancelarData_Click(object sender, EventArgs e)
        {
            // Cancela a seleção (não faz nada ou restaura valor anterior)
            MessageBox.Show("Seleção de data cancelada");
            EsconderCalendario();
        }
        private void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            // Atualiza a data temporária quando o usuário seleciona uma nova data
            dataSelecionadaTemp = e.Start;
        }
        private void EsconderCalendario()
        {
            monthCalendar.Visible = false;
            btnConfirmarData.Visible = false;
            btnCancelarData.Visible = false;
            calendarioVisivel = false;
        }

        // Configurar TimePicker
        private void InitializeTimePicker()
        {
            timePicker = new DateTimePicker();
            timePicker.Size = new Size(70, 20);
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            timePicker.Visible = false;
            timePicker.ValueChanged += TimePicker_ValueChanged;
            this.Controls.Add(timePicker);
        }

        private void BtnCancelarHora_Click(object sender, EventArgs e)
        {
            // Cancela a seleção (restaura o valor anterior)
            timePicker.Value = new DateTime(timePicker.Value.Year, timePicker.Value.Month, timePicker.Value.Day,
                                          horaSelecionadaTemp.Hours, horaSelecionadaTemp.Minutes, 0);
            MessageBox.Show("Seleção de hora cancelada");
            EsconderTimePicker();
        }
        private void TimePicker_ValueChanged(object sender, EventArgs e)
        {
            // Atualiza a hora temporária quando o usuário altera a hora
            horaSelecionadaTemp = timePicker.Value.TimeOfDay;
        }
        private void EsconderTimePicker()
        {
            timePicker.Visible = false;
            btnConfirmarHora.Visible = false;
            btnCancelarHora.Visible = false;
            timePickerVisivel = false;
        }




        private void BtEditar_Click(object sender, EventArgs e)
        {
            if (agendamentoIdSelecionado <= 0)
            {
                MessageBox.Show("Selecione um agendamento para editar.");
                return;
            }

            using (var formEditar = new Form())
            {
                formEditar.Text = "Editar Agendamento";
                formEditar.Size = new Size(350, 200);
                formEditar.StartPosition = FormStartPosition.CenterParent;
                formEditar.FormBorderStyle = FormBorderStyle.FixedDialog;
                formEditar.MaximizeBox = false;
                formEditar.MinimizeBox = false;

                // Data
                var lblData = new Label() { Text = "Data:", Top = 20, Left = 20 };
                var datePicker = new DateTimePicker()
                {
                    Top = 20,
                    Left = 120,
                    Width = 180,
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "dd/MM/yyyy"
                };

                // Hora
                var lblHora = new Label() { Text = "Hora:", Top = 60, Left = 20 };
                var timePicker = new DateTimePicker()
                {
                    Top = 60,
                    Left = 120,
                    Width = 100,
                    Format = DateTimePickerFormat.Time,
                    ShowUpDown = true
                };

                // Botão Salvar
                var btnSalvar = new Button() { Text = "Salvar", Top = 100, Left = 80, Width = 80 };
                btnSalvar.Click += (s, ev) =>
                {
                    DateTime novaData = datePicker.Value.Date;
                    TimeSpan novaHora = timePicker.Value.TimeOfDay;

                    using (var buscarAgenda = new BuscarAgenda(new DbBemAgendado()))
                    {
                        bool sucesso = buscarAgenda.AlterarDataHora(agendamentoIdSelecionado, novaData, novaHora);
                        if (sucesso)
                        {
                            MessageBox.Show("Agendamento alterado com sucesso!");
                            CarregarDados(); // atualiza a DataGridView
                            formEditar.Close();
                        }
                        else
                        {
                            MessageBox.Show("Falha ao alterar agendamento.");
                        }
                    }
                };

                // Botão Cancelar
                var btnCancelar = new Button() { Text = "Cancelar", Top = 100, Left = 170, Width = 80 };
                btnCancelar.Click += (s, ev) => formEditar.Close();

                // Adicionar controles
                formEditar.Controls.AddRange(new Control[] { lblData, datePicker, lblHora, timePicker, btnSalvar, btnCancelar });

                formEditar.ShowDialog();
            }
        }

        private void BtNovaSenha_Click(object sender, EventArgs e)
        {
            if (!UsuarioSessao.EstaLogado)
            {
                MessageBox.Show("Nenhum usuário logado!");
                return;
            }

            // Criar um formulário personalizado simples
            using (var form = new Form())
            {
                form.Text = "Alterar Senha";
                form.Size = new Size(300, 180);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                // Criar controles
                var lblSenhaAtual = new Label() { Text = "Senha Atual:", Top = 20, Left = 20, Width = 100 };
                var txtSenhaAtual = new TextBox() { Top = 20, Left = 120, Width = 150, PasswordChar = '*' };

                var lblNovaSenha = new Label() { Text = "Nova Senha:", Top = 50, Left = 20, Width = 100 };
                var txtNovaSenha = new TextBox() { Top = 50, Left = 120, Width = 150, PasswordChar = '*' };

                var lblConfirmar = new Label() { Text = "Confirmar Senha:", Top = 80, Left = 20, Width = 100 };
                var txtConfirmar = new TextBox() { Top = 80, Left = 120, Width = 150, PasswordChar = '*' };

                // Botões
                var btnOK = new Button() { Text = "OK", Top = 110, Left = 120, Width = 70 };
                var btnCancelar = new Button() { Text = "Cancelar", Top = 110, Left = 200, Width = 70 };

                // Adicionar eventos
                btnOK.Click += (s, ev) =>
                {
                    if (string.IsNullOrEmpty(txtSenhaAtual.Text))
                    {
                        MessageBox.Show("Digite a senha atual!");
                        return;
                    }

                    if (txtNovaSenha.Text != txtConfirmar.Text)
                    {
                        MessageBox.Show("As senhas não coincidem!");
                        return;
                    }

                    if (string.IsNullOrEmpty(txtNovaSenha.Text))
                    {
                        MessageBox.Show("Digite uma senha válida!");
                        return;
                    }

                    if (txtNovaSenha.Text.Length < 4)
                    {
                        MessageBox.Show("A senha deve ter pelo menos 4 caracteres!");
                        return;
                    }

                    try
                    {
                        using (var db = new DbBemAgendado())
                        using (var gerenciarUsuario = new GerenciarUsuario(db))
                        {
                            // Verificar se a senha atual está correta
                            // (Você precisará implementar este método no GerenciarUsuario)
                            bool senhaCorreta = VerificarSenhaAtual(UsuarioSessao.UsuarioLogado.Id, txtSenhaAtual.Text);

                            if (!senhaCorreta)
                            {
                                MessageBox.Show("Senha atual incorreta!");
                                return;
                            }

                            // Alterar a senha no banco
                            bool sucesso = gerenciarUsuario.TrocarSenha(UsuarioSessao.UsuarioLogado.Id, txtNovaSenha.Text);

                            if (sucesso)
                            {
                                MessageBox.Show("Senha alterada com sucesso!");
                                form.DialogResult = DialogResult.OK;
                                form.Close();
                            }
                            else
                            {
                                MessageBox.Show("Erro ao alterar senha!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro: {ex.Message}");
                    }
                };

                btnCancelar.Click += (s, ev) => { form.DialogResult = DialogResult.Cancel; form.Close(); };

                // Adicionar controles ao formulário
                form.Controls.AddRange(new Control[] {
            lblSenhaAtual, txtSenhaAtual,
            lblNovaSenha, txtNovaSenha,
            lblConfirmar, txtConfirmar,
            btnOK, btnCancelar
        });
                // Mostrar o formulário
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Atualizar a senha na sessão também
                    UsuarioSessao.UsuarioLogado.Senha = txtNovaSenha.Text;
                }
            }
        }
        private bool VerificarSenhaAtual(int usuarioId, string senha)
        {
            try
            {
                using (var db = new DbBemAgendado())
                using (var gerenciarUsuario = new GerenciarUsuario(db))
                {
                    // Você precisará implementar este método no GerenciarUsuario
                    return gerenciarUsuario.VerificarSenhaAtual(usuarioId, senha);
                }
            }
            catch
            {
                return false;
            }
        }


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
        private void TimerAtualizacao_Tick(object sender, EventArgs e)
        {
            int idSelecionado = agendamentoIdSelecionado;

            // Modifique esta linha para passar o filtro atual
            CarregarDados(apenasFuturos);

            if (idSelecionado > 0)
            {
                foreach (DataGridViewRow row in agendamento.Rows)
                {
                    if (row.Cells["ID"].Value != null &&
                        Convert.ToInt32(row.Cells["ID"].Value) == idSelecionado)
                    {
                        row.Selected = true;
                        agendamento.CurrentCell = row.Cells[1];
                        break;
                    }
                }
            }
        }

        //eventos de confirmar, cancelar e remarca retorno
        private async  void BtConfirmar_Click(object sender, EventArgs e)
        {
            if (agendamentoIdSelecionado <= 0)
            {
                MessageBox.Show("Por favor, selecione um agendamento para confirmar.",
                                "Seleção Necessária",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Pergunta de confirmação
            var resultado = MessageBox.Show($"Deseja confirmar o agendamento selecionado?",
                                           "Confirmar Agendamento",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    using (var db = new DbBemAgendado())
                    using (var buscarAgenda = new BuscarAgenda(db))
                    {
                        bool sucesso = buscarAgenda.ConfirmarAgendamento(agendamentoIdSelecionado);

                        if (sucesso)
                        {
                            MessageBox.Show("Agendamento confirmado com sucesso!",
                                           "Sucesso",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                            // Atualiza a interface
                            AtualizarStatusNaGrid(agendamentoIdSelecionado, "confirmado");

                            // Chama o envio de e-mail
                            var emailService = new EmailService(buscarAgenda);
                            await emailService.EnviarEmailConfirmacaoAPI(agendamentoIdSelecionado);

                        }
                        else
                        {
                            MessageBox.Show("Não foi possível confirmar o agendamento.",
                                           "Erro",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao confirmar agendamento: {ex.Message}",
                                   "Erro",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }

        }
        private async void BtCancelar_Click(object sender, EventArgs e)
        {
            if (agendamentoIdSelecionado <= 0)
            {
                MessageBox.Show("Por favor, selecione um agendamento para cancelar.");
                return;
            }

            var resultado = MessageBox.Show("Deseja cancelar o agendamento selecionado?",
                                           "Cancelar Agendamento",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    using (var db = new DbBemAgendado())
                    using (var buscarAgenda = new BuscarAgenda(db))
                    {
                        bool sucesso = buscarAgenda.CancelarAgendamento(agendamentoIdSelecionado);

                        if (sucesso)
                        {
                            MessageBox.Show("Agendamento cancelado com sucesso!");
                            AtualizarStatusNaGrid(agendamentoIdSelecionado, "cancelado");

                            // Chama o envio de e-mail
                            var emailService = new EmailService(buscarAgenda);
                            await emailService.EnviarEmailCancelamentoAPI(agendamentoIdSelecionado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao cancelar agendamento: {ex.Message}");
                }
            }
        }
        private void BtRetorno_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DbBemAgendado())
                using (var buscarAgenda = new BuscarAgenda(db))
                {
                    // Buscar todos os pacientes
                    var pacientes = buscarAgenda.BuscarTodosPacientes();

                    if (pacientes.Count == 0)
                    {
                        MessageBox.Show("Nenhum paciente encontrado!");
                        return;
                    }

                    // Criar lista de seleção de pacientes
                    using (var formSelecao = new Form())
                    {
                        formSelecao.Text = "Selecionar Paciente";
                        formSelecao.Size = new Size(400, 300);
                        formSelecao.StartPosition = FormStartPosition.CenterParent;

                        var listBox = new ListBox()
                        {
                            Dock = DockStyle.Fill,
                            DisplayMember = "Nome",
                            ValueMember = "Id",
                            DataSource = pacientes
                        };

                        var btnOK = new Button() { Text = "Selecionar", Dock = DockStyle.Bottom };
                        btnOK.Click += (s, ev) => formSelecao.DialogResult = DialogResult.OK;

                        formSelecao.Controls.Add(listBox);
                        formSelecao.Controls.Add(btnOK);

                        if (formSelecao.ShowDialog() == DialogResult.OK)
                        {
                            var pacienteSelecionado = (Paciente)listBox.SelectedItem;

                            // Agora pedir data e hora
                            using (var formAgendamento = new Form())
                            {
                                formAgendamento.Text = "Marcar Retorno";
                                formAgendamento.Size = new Size(420, 200);
                                formAgendamento.StartPosition = FormStartPosition.CenterParent;

                                var lblData = new Label() { Text = "Data:", Top = 20, Left = 20 };
                                var datePicker = new DateTimePicker()
                                {
                                    Top = 20,
                                    Left =120,
                                    Width = 200, 
                                    Format = DateTimePickerFormat.Custom,
                                    CustomFormat = "dd/MM/yyyy" 
                                };

                                var lblHora = new Label() { Text = "Hora:", Top = 60, Left = 20 };
                                var timePicker = new DateTimePicker()
                                {
                                    Top = 60,
                                    Left = 120,
                                    Width = 100, 
                                    Format = DateTimePickerFormat.Custom,
                                    CustomFormat = "HH:mm",
                                    ShowUpDown = true
                                };

                                var btnSalvar = new Button() { Text = "Salvar", Top = 100, Left = 100 };
                                btnSalvar.Click += (s, ev) =>
                                {
                                    DateTime data = datePicker.Value.Date;
                                    TimeSpan hora = timePicker.Value.TimeOfDay;

                                    bool sucesso = buscarAgenda.InserirAgendamento(
                                        pacienteSelecionado.Id, data, hora);

                                    if (sucesso)
                                    {
                                        MessageBox.Show("Retorno cadastrado com sucesso como PENDENTE!");
                                        CarregarDados(); // Atualiza a grid
                                        formAgendamento.DialogResult = DialogResult.OK;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erro ao cadastrar retorno!");
                                    }
                                };

                                formAgendamento.Controls.Add(lblData);
                                formAgendamento.Controls.Add(datePicker);
                                formAgendamento.Controls.Add(lblHora);
                                formAgendamento.Controls.Add(timePicker);
                                formAgendamento.Controls.Add(btnSalvar);

                                formAgendamento.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }
        private void AtualizarStatusNaGrid(int agendamentoId, string novoStatus)
        {
            // Encontra a linha correspondente ao ID e atualiza o status
            foreach (DataGridViewRow row in agendamento.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == agendamentoId)
                {
                    row.Cells["Status"].Value = novoStatus;

                    // Força o redesenho para aplicar a formatação de cores
                    agendamento.InvalidateRow(row.Index);
                    break;
                }
            }
        }
  
    }
}