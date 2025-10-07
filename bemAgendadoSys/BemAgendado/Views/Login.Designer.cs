namespace BemAgendado.Views
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.TxtSenhas = new System.Windows.Forms.TextBox();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.LblSenha = new System.Windows.Forms.Label();
            this.BtEntrar = new System.Windows.Forms.Button();
            this.LblEsqueciSenha = new System.Windows.Forms.LinkLabel();
            this.BtOcutarSenha = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-131, -19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(633, 361);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.Location = new System.Drawing.Point(442, 101);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(140, 20);
            this.TxtUsuario.TabIndex = 3;
            // 
            // TxtSenhas
            // 
            this.TxtSenhas.Location = new System.Drawing.Point(442, 159);
            this.TxtSenhas.Name = "TxtSenhas";
            this.TxtSenhas.PasswordChar = '*';
            this.TxtSenhas.Size = new System.Drawing.Size(140, 20);
            this.TxtSenhas.TabIndex = 4;
            // 
            // LblUsuario
            // 
            this.LblUsuario.AutoSize = true;
            this.LblUsuario.Font = new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUsuario.Location = new System.Drawing.Point(438, 77);
            this.LblUsuario.Name = "LblUsuario";
            this.LblUsuario.Size = new System.Drawing.Size(64, 21);
            this.LblUsuario.TabIndex = 5;
            this.LblUsuario.Text = "Usuário";
            // 
            // LblSenha
            // 
            this.LblSenha.AutoSize = true;
            this.LblSenha.Font = new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSenha.Location = new System.Drawing.Point(438, 135);
            this.LblSenha.Name = "LblSenha";
            this.LblSenha.Size = new System.Drawing.Size(53, 21);
            this.LblSenha.TabIndex = 6;
            this.LblSenha.Text = "Senha";
            // 
            // BtEntrar
            // 
            this.BtEntrar.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtEntrar.Location = new System.Drawing.Point(467, 206);
            this.BtEntrar.Name = "BtEntrar";
            this.BtEntrar.Size = new System.Drawing.Size(91, 26);
            this.BtEntrar.TabIndex = 7;
            this.BtEntrar.Text = "Entrar";
            this.BtEntrar.UseVisualStyleBackColor = false;
            this.BtEntrar.Click += new System.EventHandler(this.BtEntrar_Click);
            // 
            // LblEsqueciSenha
            // 
            this.LblEsqueciSenha.AutoSize = true;
            this.LblEsqueciSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEsqueciSenha.Location = new System.Drawing.Point(464, 258);
            this.LblEsqueciSenha.Name = "LblEsqueciSenha";
            this.LblEsqueciSenha.Size = new System.Drawing.Size(86, 13);
            this.LblEsqueciSenha.TabIndex = 8;
            this.LblEsqueciSenha.TabStop = true;
            this.LblEsqueciSenha.Text = "Esqueci a senha";
            this.LblEsqueciSenha.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblEsqueciSenha_LinkClicked);
            // 
            // BtOcutarSenha
            // 
            this.BtOcutarSenha.BackColor = System.Drawing.Color.Transparent;
            this.BtOcutarSenha.ForeColor = System.Drawing.Color.White;
            this.BtOcutarSenha.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.BtOcutarSenha.IconColor = System.Drawing.Color.Black;
            this.BtOcutarSenha.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtOcutarSenha.IconSize = 20;
            this.BtOcutarSenha.Location = new System.Drawing.Point(576, 159);
            this.BtOcutarSenha.Name = "BtOcutarSenha";
            this.BtOcutarSenha.Size = new System.Drawing.Size(30, 21);
            this.BtOcutarSenha.TabIndex = 24;
            this.BtOcutarSenha.UseVisualStyleBackColor = false;
            this.BtOcutarSenha.Click += new System.EventHandler(this.BtOcutarSenha_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(686, 339);
            this.Controls.Add(this.BtOcutarSenha);
            this.Controls.Add(this.LblEsqueciSenha);
            this.Controls.Add(this.BtEntrar);
            this.Controls.Add(this.LblSenha);
            this.Controls.Add(this.LblUsuario);
            this.Controls.Add(this.TxtSenhas);
            this.Controls.Add(this.TxtUsuario);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.TextBox TxtSenhas;
        private System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.Label LblSenha;
        private System.Windows.Forms.Button BtEntrar;
        private System.Windows.Forms.LinkLabel LblEsqueciSenha;
        private FontAwesome.Sharp.IconButton BtOcutarSenha;
    }
}