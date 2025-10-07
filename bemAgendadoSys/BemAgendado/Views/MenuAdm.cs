using BemAgendado.Controler;
using BemAgendado.Model;
using FontAwesome.Sharp;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BemAgendado.Views
{
    public partial class MenuAdm : Form
    {
        private List<Usuario> _usuarios;
        private Usuario _usuarioSelecionado;
        private bool _modoEdicao = false;

        public MenuAdm()
        {
            InitializeComponent();
            InitializeUsuarioInfo();
            CarregarTodosUsuarios();
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



        private void ConfigurarGrid()
        {
            TodosOsUsuarios.EnableHeadersVisualStyles = false;
            TodosOsUsuarios.RowTemplate.Height = 35;
            TodosOsUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            TodosOsUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            TodosOsUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            TodosOsUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TodosOsUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            TodosOsUsuarios.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            TodosOsUsuarios.DefaultCellStyle.SelectionForeColor = Color.Black;
            TodosOsUsuarios.RowHeadersVisible = false;

            if (TodosOsUsuarios.DataSource is List<Usuario> && TodosOsUsuarios.RowCount > 0)
            {
                if (TodosOsUsuarios.Columns.Contains("Senha")) TodosOsUsuarios.Columns["Senha"].Visible = false;
                if (TodosOsUsuarios.Columns.Contains("Id")) TodosOsUsuarios.Columns["Id"].HeaderText = "ID";
                if (TodosOsUsuarios.Columns.Contains("NomeUsuario")) TodosOsUsuarios.Columns["NomeUsuario"].HeaderText = "Nome do Usuário";
                if (TodosOsUsuarios.Columns.Contains("TipoDeUsuario")) TodosOsUsuarios.Columns["TipoDeUsuario"].HeaderText = "Tipo";
                if (TodosOsUsuarios.Columns.Contains("Id")) TodosOsUsuarios.Columns["Id"].Width = 80;
                if (TodosOsUsuarios.Columns.Contains("TipoDeUsuario")) TodosOsUsuarios.Columns["TipoDeUsuario"].Width = 200;
                if (TodosOsUsuarios.Columns.Contains("NomeUsuario")) TodosOsUsuarios.Columns["NomeUsuario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void CarregarTodosUsuarios()
        {
            try
            {
                using (var dbConnection = new DbBemAgendado())
                using (var buscarUsuario = new BuscarUsuario(dbConnection))
                {
                    _usuarios = buscarUsuario.BuscarTodosUsuarios(); // Armazena a lista completa
                    TodosOsUsuarios.DataSource = null;
                    TodosOsUsuarios.DataSource = _usuarios; // A grid sempre mostra a lista completa inicialmente
                    ConfigurarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar todos os usuários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TodosOsUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma célula válida (não no cabeçalho)
            if (e.RowIndex >= 0 && e.RowIndex < TodosOsUsuarios.RowCount)
            {
                // Obtém o usuário selecionado
                var usuarioSelecionado = TodosOsUsuarios.Rows[e.RowIndex].DataBoundItem as Usuario;

                if (usuarioSelecionado != null)
                {
                    _usuarioSelecionado = usuarioSelecionado;
                    PreencherCampos(usuarioSelecionado);
                    HabilitarCampos(true);
                    _modoEdicao = true;

                    // Opcional: destacar visualmente a linha selecionada
                    TodosOsUsuarios.ClearSelection();
                    TodosOsUsuarios.Rows[e.RowIndex].Selected = true;
                }
            }

        }





        private void BtPesquisar_Click(object sender, EventArgs e)
        {
            string termoBusca = TxtPesquisa.Text.Trim();
            if (string.IsNullOrEmpty(termoBusca))
            {
                MessageBox.Show("Por favor, digite um nome ou ID para pesquisar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            var usuariosEncontrados = _usuarios.Where(u =>
                   u.NomeUsuario.ToLower().Contains(termoBusca.ToLower()) ||
                   u.Id.ToString().Contains(termoBusca)
               ).ToList();

         
            if (usuariosEncontrados.Count == 1)
            {
                var usuarioParaCarregar = usuariosEncontrados.First();
                _usuarioSelecionado = usuarioParaCarregar; 
                PreencherCampos(usuarioParaCarregar);
                HabilitarCampos(true);
                _modoEdicao = true;
            }
           
            else if (usuariosEncontrados.Count == 0)
            {
                MessageBox.Show("Nenhum usuário encontrado com este termo.", "Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
            }
        }
        
        private void BtCriarUsuario_Click(object sender, EventArgs e)
        {
            LimparCampos();
            HabilitarCampos(true);
            _modoEdicao = false;
            _usuarioSelecionado = null;
            TxtNome.Focus();
        }

        private void BtExcluirUsuario_Click(object sender, EventArgs e)
        {
            if (_usuarioSelecionado == null)
            {
                MessageBox.Show("Você precisa pesquisar e carregar um usuário antes de excluir.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Deseja realmente excluir o usuário '{_usuarioSelecionado.NomeUsuario}'?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var dbConnection = new DbBemAgendado())
                    using (var gerenciarUsuario = new GerenciarUsuario(dbConnection))
                    {
                        gerenciarUsuario.ExcluirUsuario(_usuarioSelecionado.Id);
                        MessageBox.Show("Usuário excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                        CarregarTodosUsuarios();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir usuário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                using (var dbConnection = new DbBemAgendado())
                using (var gerenciarUsuario = new GerenciarUsuario(dbConnection))
                {
                    if (_modoEdicao)
                    {
                        if (_usuarioSelecionado == null)
                        {
                            MessageBox.Show("Nenhum usuário carregado para editar. A operação foi cancelada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        _usuarioSelecionado.NomeUsuario = TxtNome.Text;
                        _usuarioSelecionado.Senha = TxtSenha.Text;
                        _usuarioSelecionado.TipoDeUsuario = TxtTipoDeUsuario.SelectedItem.ToString();

                        gerenciarUsuario.EditarUsuario(_usuarioSelecionado);
                        MessageBox.Show("Usuário editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                       
                        var novoUsuario = new Usuario
                        {
                            NomeUsuario = TxtNome.Text,
                            Senha = TxtSenha.Text,
                            TipoDeUsuario = TxtTipoDeUsuario.SelectedItem?.ToString()
                        };

                        gerenciarUsuario.CriarUsuario(novoUsuario);
                        MessageBox.Show("Usuário criado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LimparCampos();
                    HabilitarCampos(false);
                    CarregarTodosUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar usuário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(TxtNome.Text))
            {
                MessageBox.Show("Digite o nome do usuário.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtNome.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(TxtSenha.Text))
            {
                MessageBox.Show("Digite a senha do usuário.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtSenha.Focus();
                return false;
            }
            if (TxtTipoDeUsuario.SelectedItem == null)
            {
                MessageBox.Show("Selecione o tipo de usuário.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtTipoDeUsuario.Focus();
                return false;
            }
            return true;
        }

        private void PreencherCampos(Usuario usuario)
        {
            if (usuario != null)
            {
                TxtNome.Text = usuario.NomeUsuario;
                TxtSenha.Text = usuario.Senha;

               TxtTipoDeUsuario.SelectedItem = usuario.TipoDeUsuario;
            }
        }

        private void LimparCampos()
        {
            TxtNome.Clear();
            TxtSenha.Clear();
            TxtTipoDeUsuario.SelectedIndex = -1;
            TxtPesquisa.Clear();
            _usuarioSelecionado = null;
            _modoEdicao = false;
            TodosOsUsuarios.ClearSelection();
        }

        private void HabilitarCampos(bool habilitar)
        {
            TxtNome.Enabled = habilitar;
            TxtSenha.Enabled = habilitar;
            TxtTipoDeUsuario.Enabled = habilitar;
            BtSalvar.Enabled = habilitar;
        }

        
        private void TodosOsUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este método está aqui apenas para evitar o erro de compilação.
            // A nossa lógica principal está no evento SelectionChanged.
        }

        private void BtOcutarSenha_Click(object sender, EventArgs e)
        {
            if (TxtSenha.PasswordChar == '*')
            {
                TxtSenha.PasswordChar = '\0'; // Mostra a senha
                ((IconButton)sender).IconChar = IconChar.EyeSlash; // Olho cortado
            }
            else
            {
                TxtSenha.PasswordChar = '*'; // Oculta a senha
                ((IconButton)sender).IconChar = IconChar.Eye; // Olho aberto
            }
        }
    }
}