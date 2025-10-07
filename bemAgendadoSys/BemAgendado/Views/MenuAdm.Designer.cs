namespace BemAgendado.Views
{
    partial class MenuAdm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuAdm));
            this.BtExcluirUsuario = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtCriarUsuario = new FontAwesome.Sharp.IconButton();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.BtPesquisar = new FontAwesome.Sharp.IconButton();
            this.TxtNome = new System.Windows.Forms.TextBox();
            this.LblNome = new MaterialSkin.Controls.MaterialLabel();
            this.LblSenha = new MaterialSkin.Controls.MaterialLabel();
            this.TxtSenha = new System.Windows.Forms.TextBox();
            this.LblTipoDeUsuario = new MaterialSkin.Controls.MaterialLabel();
            this.TxtTipoDeUsuario = new System.Windows.Forms.ComboBox();
            this.BtSalvar = new FontAwesome.Sharp.IconButton();
            this.TodosOsUsuarios = new System.Windows.Forms.DataGridView();
            this.LblUsuarioLogado = new System.Windows.Forms.Label();
            this.BtOcutarSenha = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TodosOsUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // BtExcluirUsuario
            // 
            this.BtExcluirUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtExcluirUsuario.BackColor = System.Drawing.Color.White;
            this.BtExcluirUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtExcluirUsuario.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtExcluirUsuario.ForeColor = System.Drawing.Color.Red;
            this.BtExcluirUsuario.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtExcluirUsuario.IconColor = System.Drawing.Color.Black;
            this.BtExcluirUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtExcluirUsuario.Location = new System.Drawing.Point(769, 11);
            this.BtExcluirUsuario.Name = "BtExcluirUsuario";
            this.BtExcluirUsuario.Size = new System.Drawing.Size(106, 33);
            this.BtExcluirUsuario.TabIndex = 10;
            this.BtExcluirUsuario.Text = "Excluir ";
            this.BtExcluirUsuario.UseVisualStyleBackColor = false;
            this.BtExcluirUsuario.Click += new System.EventHandler(this.BtExcluirUsuario_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-73, -28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(333, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // BtCriarUsuario
            // 
            this.BtCriarUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtCriarUsuario.BackColor = System.Drawing.Color.White;
            this.BtCriarUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtCriarUsuario.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtCriarUsuario.ForeColor = System.Drawing.Color.Green;
            this.BtCriarUsuario.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtCriarUsuario.IconColor = System.Drawing.Color.Black;
            this.BtCriarUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtCriarUsuario.Location = new System.Drawing.Point(590, 11);
            this.BtCriarUsuario.Name = "BtCriarUsuario";
            this.BtCriarUsuario.Size = new System.Drawing.Size(120, 33);
            this.BtCriarUsuario.TabIndex = 7;
            this.BtCriarUsuario.Text = "Criar Usuário";
            this.BtCriarUsuario.UseVisualStyleBackColor = false;
            this.BtCriarUsuario.Click += new System.EventHandler(this.BtCriarUsuario_Click);
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(347, 24);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(174, 20);
            this.TxtPesquisa.TabIndex = 1;
            // 
            // BtPesquisar
            // 
            this.BtPesquisar.AutoEllipsis = true;
            this.BtPesquisar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.BtPesquisar.IconColor = System.Drawing.Color.Black;
            this.BtPesquisar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtPesquisar.IconSize = 15;
            this.BtPesquisar.Location = new System.Drawing.Point(482, 24);
            this.BtPesquisar.Name = "BtPesquisar";
            this.BtPesquisar.Size = new System.Drawing.Size(39, 20);
            this.BtPesquisar.TabIndex = 2;
            this.BtPesquisar.UseVisualStyleBackColor = true;
            this.BtPesquisar.Click += new System.EventHandler(this.BtPesquisar_Click);
            // 
            // TxtNome
            // 
            this.TxtNome.Location = new System.Drawing.Point(195, 206);
            this.TxtNome.Name = "TxtNome";
            this.TxtNome.Size = new System.Drawing.Size(142, 20);
            this.TxtNome.TabIndex = 3;
            // 
            // LblNome
            // 
            this.LblNome.AutoSize = true;
            this.LblNome.Depth = 0;
            this.LblNome.Font = new System.Drawing.Font("Roboto", 11F);
            this.LblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LblNome.Location = new System.Drawing.Point(191, 184);
            this.LblNome.MouseState = MaterialSkin.MouseState.HOVER;
            this.LblNome.Name = "LblNome";
            this.LblNome.Size = new System.Drawing.Size(50, 19);
            this.LblNome.TabIndex = 16;
            this.LblNome.Text = "Nome";
            // 
            // LblSenha
            // 
            this.LblSenha.AutoSize = true;
            this.LblSenha.Depth = 0;
            this.LblSenha.Font = new System.Drawing.Font("Roboto", 11F);
            this.LblSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LblSenha.Location = new System.Drawing.Point(418, 184);
            this.LblSenha.MouseState = MaterialSkin.MouseState.HOVER;
            this.LblSenha.Name = "LblSenha";
            this.LblSenha.Size = new System.Drawing.Size(50, 19);
            this.LblSenha.TabIndex = 18;
            this.LblSenha.Text = "Senha";
            // 
            // TxtSenha
            // 
            this.TxtSenha.Location = new System.Drawing.Point(422, 206);
            this.TxtSenha.Name = "TxtSenha";
            this.TxtSenha.Size = new System.Drawing.Size(142, 20);
            this.TxtSenha.TabIndex = 4;
            // 
            // LblTipoDeUsuario
            // 
            this.LblTipoDeUsuario.AutoSize = true;
            this.LblTipoDeUsuario.Depth = 0;
            this.LblTipoDeUsuario.Font = new System.Drawing.Font("Roboto", 11F);
            this.LblTipoDeUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LblTipoDeUsuario.Location = new System.Drawing.Point(639, 184);
            this.LblTipoDeUsuario.MouseState = MaterialSkin.MouseState.HOVER;
            this.LblTipoDeUsuario.Name = "LblTipoDeUsuario";
            this.LblTipoDeUsuario.Size = new System.Drawing.Size(117, 19);
            this.LblTipoDeUsuario.TabIndex = 20;
            this.LblTipoDeUsuario.Text = "Tipo De Usuario";
            // 
            // TxtTipoDeUsuario
            // 
            this.TxtTipoDeUsuario.FormattingEnabled = true;
            this.TxtTipoDeUsuario.Items.AddRange(new object[] {
            "Adm",
            "Colaborador"});
            this.TxtTipoDeUsuario.Location = new System.Drawing.Point(643, 206);
            this.TxtTipoDeUsuario.Name = "TxtTipoDeUsuario";
            this.TxtTipoDeUsuario.Size = new System.Drawing.Size(142, 21);
            this.TxtTipoDeUsuario.TabIndex = 5;
            // 
            // BtSalvar
            // 
            this.BtSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtSalvar.BackColor = System.Drawing.Color.White;
            this.BtSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtSalvar.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtSalvar.ForeColor = System.Drawing.Color.DarkOrange;
            this.BtSalvar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtSalvar.IconColor = System.Drawing.Color.Black;
            this.BtSalvar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtSalvar.Location = new System.Drawing.Point(755, 479);
            this.BtSalvar.Name = "BtSalvar";
            this.BtSalvar.Size = new System.Drawing.Size(120, 33);
            this.BtSalvar.TabIndex = 6;
            this.BtSalvar.Text = "Salvar";
            this.BtSalvar.UseVisualStyleBackColor = false;
            this.BtSalvar.Click += new System.EventHandler(this.BtSalvar_Click);
            // 
            // TodosOsUsuarios
            // 
            this.TodosOsUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.TodosOsUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TodosOsUsuarios.Location = new System.Drawing.Point(217, 285);
            this.TodosOsUsuarios.Name = "TodosOsUsuarios";
            this.TodosOsUsuarios.Size = new System.Drawing.Size(493, 176);
            this.TodosOsUsuarios.TabIndex = 21;
            this.TodosOsUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TodosOsUsuarios_CellContentClick);
            this.TodosOsUsuarios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TodosOsUsuarios_CellDoubleClick);
            // 
            // LblUsuarioLogado
            // 
            this.LblUsuarioLogado.AutoSize = true;
            this.LblUsuarioLogado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUsuarioLogado.Location = new System.Drawing.Point(0, 137);
            this.LblUsuarioLogado.Name = "LblUsuarioLogado";
            this.LblUsuarioLogado.Size = new System.Drawing.Size(178, 25);
            this.LblUsuarioLogado.TabIndex = 22;
            this.LblUsuarioLogado.Text = "Nome do Usuario";
            // 
            // BtOcutarSenha
            // 
            this.BtOcutarSenha.BackColor = System.Drawing.Color.Transparent;
            this.BtOcutarSenha.ForeColor = System.Drawing.Color.White;
            this.BtOcutarSenha.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.BtOcutarSenha.IconColor = System.Drawing.Color.Black;
            this.BtOcutarSenha.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtOcutarSenha.IconSize = 20;
            this.BtOcutarSenha.Location = new System.Drawing.Point(557, 205);
            this.BtOcutarSenha.Name = "BtOcutarSenha";
            this.BtOcutarSenha.Size = new System.Drawing.Size(30, 21);
            this.BtOcutarSenha.TabIndex = 23;
            this.BtOcutarSenha.UseVisualStyleBackColor = false;
            this.BtOcutarSenha.Click += new System.EventHandler(this.BtOcutarSenha_Click);
            // 
            // MenuAdm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(906, 536);
            this.Controls.Add(this.BtOcutarSenha);
            this.Controls.Add(this.LblUsuarioLogado);
            this.Controls.Add(this.TodosOsUsuarios);
            this.Controls.Add(this.BtSalvar);
            this.Controls.Add(this.TxtTipoDeUsuario);
            this.Controls.Add(this.LblTipoDeUsuario);
            this.Controls.Add(this.LblSenha);
            this.Controls.Add(this.TxtSenha);
            this.Controls.Add(this.LblNome);
            this.Controls.Add(this.TxtNome);
            this.Controls.Add(this.BtPesquisar);
            this.Controls.Add(this.TxtPesquisa);
            this.Controls.Add(this.BtCriarUsuario);
            this.Controls.Add(this.BtExcluirUsuario);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MenuAdm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuAdm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TodosOsUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FontAwesome.Sharp.IconButton BtExcluirUsuario;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton BtCriarUsuario;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private FontAwesome.Sharp.IconButton BtPesquisar;
        private System.Windows.Forms.TextBox TxtNome;
        private MaterialSkin.Controls.MaterialLabel LblNome;
        private MaterialSkin.Controls.MaterialLabel LblSenha;
        private System.Windows.Forms.TextBox TxtSenha;
        private MaterialSkin.Controls.MaterialLabel LblTipoDeUsuario;
        private System.Windows.Forms.ComboBox TxtTipoDeUsuario;
        private FontAwesome.Sharp.IconButton BtSalvar;
        private System.Windows.Forms.DataGridView TodosOsUsuarios;
        private System.Windows.Forms.Label LblUsuarioLogado;
        private FontAwesome.Sharp.IconButton BtOcutarSenha;
    }
}