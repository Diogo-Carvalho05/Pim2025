using BemAgendado.Controler;
using BemAgendado.Model;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BemAgendado.Views
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtEntrar_Click(object sender, EventArgs e)
        {
            string usuario = TxtUsuario.Text;
            string senha = TxtSenhas.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var dbConnection = new DbBemAgendado())
                using (var verificador = new VerificarLogin(dbConnection))
                {
                    Usuario usuarioAutenticado = verificador.VerificarLoginRetornarUsuario(usuario, senha);

                    if (usuarioAutenticado != null)
                    {
                        // Armazena o usuário na sessão
                        UsuarioSessao.UsuarioLogado = usuarioAutenticado;

                        string mensagemTipo;
                        Form menu;

                        switch (usuarioAutenticado.TipoDeUsuario.ToLower())
                        {
                            case "adm":
                                mensagemTipo = "Administrador";
                                menu = new MenuAdm();
                                break;
                            case "colaborador":
                                mensagemTipo = "Colaborador";
                                menu = new Menu();
                                break;
                            default:
                                mensagemTipo = "Tipo desconhecido";
                                throw new Exception("Tipo de usuário não suportado");
                        }

                        MessageBox.Show($"Login realizado com sucesso!", "Bem Agendado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        menu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos.", "Erro de Login",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LblEsqueciSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Contate o suporte para recuperar sua senha.", "Esqueci minha senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtOcutarSenha_Click(object sender, EventArgs e)
        {
            if (TxtSenhas.PasswordChar == '*')
            {
                TxtSenhas.PasswordChar = '\0'; // Mostra a senha
                ((IconButton)sender).IconChar = IconChar.EyeSlash; // Olho cortado
            }
            else
            {
                TxtSenhas.PasswordChar = '*'; // Oculta a senha
                ((IconButton)sender).IconChar = IconChar.Eye; // Olho aberto
            }
        }
    }
}
