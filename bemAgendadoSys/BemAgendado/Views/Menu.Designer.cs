namespace BemAgendado.Views
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtCancelar = new FontAwesome.Sharp.IconButton();
            this.BtConfirmar = new FontAwesome.Sharp.IconButton();
            this.BtNovaSenha = new FontAwesome.Sharp.IconButton();
            this.LblUsuarioLogado = new System.Windows.Forms.Label();
            this.agendamento = new System.Windows.Forms.DataGridView();
            this.BtRetorno = new FontAwesome.Sharp.IconButton();
            this.BtEditar = new FontAwesome.Sharp.IconButton();
            this.btnProximosAgendamentos = new FontAwesome.Sharp.IconButton();
            this.btnTodosAgendamentos = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.agendamento)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-54, -27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(319, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // BtCancelar
            // 
            this.BtCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtCancelar.BackColor = System.Drawing.Color.White;
            this.BtCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtCancelar.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtCancelar.ForeColor = System.Drawing.Color.Red;
            this.BtCancelar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtCancelar.IconColor = System.Drawing.Color.Black;
            this.BtCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtCancelar.Location = new System.Drawing.Point(890, 12);
            this.BtCancelar.Name = "BtCancelar";
            this.BtCancelar.Size = new System.Drawing.Size(106, 33);
            this.BtCancelar.TabIndex = 4;
            this.BtCancelar.Text = "Cancelar";
            this.BtCancelar.UseVisualStyleBackColor = false;
            this.BtCancelar.Click += new System.EventHandler(this.BtCancelar_Click);
            // 
            // BtConfirmar
            // 
            this.BtConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtConfirmar.BackColor = System.Drawing.Color.White;
            this.BtConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtConfirmar.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtConfirmar.ForeColor = System.Drawing.Color.Green;
            this.BtConfirmar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtConfirmar.IconColor = System.Drawing.Color.Black;
            this.BtConfirmar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtConfirmar.Location = new System.Drawing.Point(767, 12);
            this.BtConfirmar.Name = "BtConfirmar";
            this.BtConfirmar.Size = new System.Drawing.Size(106, 33);
            this.BtConfirmar.TabIndex = 3;
            this.BtConfirmar.Text = "Confirmar";
            this.BtConfirmar.UseVisualStyleBackColor = false;
            this.BtConfirmar.Click += new System.EventHandler(this.BtConfirmar_Click);
            // 
            // BtNovaSenha
            // 
            this.BtNovaSenha.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtNovaSenha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtNovaSenha.ForeColor = System.Drawing.Color.MediumBlue;
            this.BtNovaSenha.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this.BtNovaSenha.IconColor = System.Drawing.Color.MediumBlue;
            this.BtNovaSenha.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtNovaSenha.IconSize = 20;
            this.BtNovaSenha.Location = new System.Drawing.Point(271, 17);
            this.BtNovaSenha.Name = "BtNovaSenha";
            this.BtNovaSenha.Size = new System.Drawing.Size(28, 28);
            this.BtNovaSenha.TabIndex = 5;
            this.BtNovaSenha.UseVisualStyleBackColor = false;
            this.BtNovaSenha.Click += new System.EventHandler(this.BtNovaSenha_Click);
            // 
            // LblUsuarioLogado
            // 
            this.LblUsuarioLogado.AutoSize = true;
            this.LblUsuarioLogado.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUsuarioLogado.Location = new System.Drawing.Point(1, 132);
            this.LblUsuarioLogado.Name = "LblUsuarioLogado";
            this.LblUsuarioLogado.Size = new System.Drawing.Size(224, 31);
            this.LblUsuarioLogado.TabIndex = 9;
            this.LblUsuarioLogado.Text = "Nome do Usuario";
            // 
            // agendamento
            // 
            this.agendamento.BackgroundColor = System.Drawing.Color.White;
            this.agendamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.agendamento.Location = new System.Drawing.Point(232, 190);
            this.agendamento.Name = "agendamento";
            this.agendamento.Size = new System.Drawing.Size(683, 462);
            this.agendamento.TabIndex = 6;
            this.agendamento.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.agendamento_CellContentClick);
            this.agendamento.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.agendamento_CellFormatting);
            // 
            // BtRetorno
            // 
            this.BtRetorno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtRetorno.BackColor = System.Drawing.Color.White;
            this.BtRetorno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtRetorno.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtRetorno.ForeColor = System.Drawing.Color.Orange;
            this.BtRetorno.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtRetorno.IconColor = System.Drawing.Color.Black;
            this.BtRetorno.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtRetorno.Location = new System.Drawing.Point(644, 12);
            this.BtRetorno.Name = "BtRetorno";
            this.BtRetorno.Size = new System.Drawing.Size(106, 33);
            this.BtRetorno.TabIndex = 10;
            this.BtRetorno.Text = "Retorno";
            this.BtRetorno.UseVisualStyleBackColor = false;
            this.BtRetorno.Click += new System.EventHandler(this.BtRetorno_Click);
            // 
            // BtEditar
            // 
            this.BtEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtEditar.BackColor = System.Drawing.Color.White;
            this.BtEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtEditar.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtEditar.ForeColor = System.Drawing.Color.MediumBlue;
            this.BtEditar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtEditar.IconColor = System.Drawing.Color.Black;
            this.BtEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtEditar.Location = new System.Drawing.Point(521, 12);
            this.BtEditar.Name = "BtEditar";
            this.BtEditar.Size = new System.Drawing.Size(106, 33);
            this.BtEditar.TabIndex = 11;
            this.BtEditar.Text = "Editar";
            this.BtEditar.UseVisualStyleBackColor = false;
            this.BtEditar.Click += new System.EventHandler(this.BtEditar_Click);
            // 
            // btnProximosAgendamentos
            // 
            this.btnProximosAgendamentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProximosAgendamentos.BackColor = System.Drawing.Color.White;
            this.btnProximosAgendamentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProximosAgendamentos.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnProximosAgendamentos.IconChar = FontAwesome.Sharp.IconChar.TableCellsRowLock;
            this.btnProximosAgendamentos.IconColor = System.Drawing.Color.Black;
            this.btnProximosAgendamentos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnProximosAgendamentos.IconSize = 25;
            this.btnProximosAgendamentos.Location = new System.Drawing.Point(421, 17);
            this.btnProximosAgendamentos.Name = "btnProximosAgendamentos";
            this.btnProximosAgendamentos.Size = new System.Drawing.Size(44, 30);
            this.btnProximosAgendamentos.TabIndex = 12;
            this.btnProximosAgendamentos.UseVisualStyleBackColor = false;
            // 
            // btnTodosAgendamentos
            // 
            this.btnTodosAgendamentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTodosAgendamentos.BackColor = System.Drawing.Color.White;
            this.btnTodosAgendamentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTodosAgendamentos.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnTodosAgendamentos.IconChar = FontAwesome.Sharp.IconChar.TableCellsRowUnlock;
            this.btnTodosAgendamentos.IconColor = System.Drawing.Color.Black;
            this.btnTodosAgendamentos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTodosAgendamentos.IconSize = 25;
            this.btnTodosAgendamentos.Location = new System.Drawing.Point(471, 16);
            this.btnTodosAgendamentos.Name = "btnTodosAgendamentos";
            this.btnTodosAgendamentos.Size = new System.Drawing.Size(44, 30);
            this.btnTodosAgendamentos.TabIndex = 13;
            this.btnTodosAgendamentos.UseVisualStyleBackColor = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.btnTodosAgendamentos);
            this.Controls.Add(this.btnProximosAgendamentos);
            this.Controls.Add(this.BtEditar);
            this.Controls.Add(this.BtRetorno);
            this.Controls.Add(this.agendamento);
            this.Controls.Add(this.LblUsuarioLogado);
            this.Controls.Add(this.BtNovaSenha);
            this.Controls.Add(this.BtConfirmar);
            this.Controls.Add(this.BtCancelar);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.agendamento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton BtCancelar;
        private FontAwesome.Sharp.IconButton BtConfirmar;
        private FontAwesome.Sharp.IconButton BtNovaSenha;
        private System.Windows.Forms.Label LblUsuarioLogado;
        private System.Windows.Forms.DataGridView agendamento;
        private FontAwesome.Sharp.IconButton BtRetorno;
        private FontAwesome.Sharp.IconButton BtEditar;
        private FontAwesome.Sharp.IconButton btnProximosAgendamentos;
        private FontAwesome.Sharp.IconButton btnTodosAgendamentos;
    }
}