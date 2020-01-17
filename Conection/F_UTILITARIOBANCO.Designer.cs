namespace Conection
{
    partial class F_UTILITARIOBANCO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_UTILITARIOBANCO));
            this.pnlComponentes = new System.Windows.Forms.Panel();
            this.lblProgresso = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRealizaBackup = new System.Windows.Forms.Button();
            this.buttonSelecionarArquivoBackup = new System.Windows.Forms.Button();
            this.textBoxArquivoBackup = new System.Windows.Forms.TextBox();
            this.sFDialogArquivoBackup = new System.Windows.Forms.SaveFileDialog();
            this.oFDialogArquivoRestauracao = new System.Windows.Forms.OpenFileDialog();
            this.sFDialogCopiaFisica = new System.Windows.Forms.SaveFileDialog();
            this.bgWCopia = new System.ComponentModel.BackgroundWorker();
            this.pgbProgresso = new System.Windows.Forms.ProgressBar();
            this.pnlComponentes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlComponentes
            // 
            this.pnlComponentes.BackColor = System.Drawing.Color.White;
            this.pnlComponentes.Controls.Add(this.pgbProgresso);
            this.pnlComponentes.Controls.Add(this.lblProgresso);
            this.pnlComponentes.Controls.Add(this.label2);
            this.pnlComponentes.Controls.Add(this.label1);
            this.pnlComponentes.Controls.Add(this.buttonRealizaBackup);
            this.pnlComponentes.Controls.Add(this.buttonSelecionarArquivoBackup);
            this.pnlComponentes.Controls.Add(this.textBoxArquivoBackup);
            this.pnlComponentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlComponentes.Location = new System.Drawing.Point(0, 0);
            this.pnlComponentes.Name = "pnlComponentes";
            this.pnlComponentes.Size = new System.Drawing.Size(466, 202);
            this.pnlComponentes.TabIndex = 0;
            // 
            // lblProgresso
            // 
            this.lblProgresso.AutoSize = true;
            this.lblProgresso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgresso.Location = new System.Drawing.Point(12, 150);
            this.lblProgresso.Name = "lblProgresso";
            this.lblProgresso.Size = new System.Drawing.Size(0, 15);
            this.lblProgresso.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 82);
            this.label2.TabIndex = 11;
            this.label2.Text = "Atenção\r\nRecomenda-se que os arquivos de backup sejam salvos em mídias externas, " +
    "para preservar e garantir a integridade dos dados da aplicação.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Arquivo de backup";
            // 
            // buttonRealizaBackup
            // 
            this.buttonRealizaBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRealizaBackup.Image = ((System.Drawing.Image)(resources.GetObject("buttonRealizaBackup.Image")));
            this.buttonRealizaBackup.Location = new System.Drawing.Point(347, 64);
            this.buttonRealizaBackup.Name = "buttonRealizaBackup";
            this.buttonRealizaBackup.Size = new System.Drawing.Size(107, 75);
            this.buttonRealizaBackup.TabIndex = 5;
            this.buttonRealizaBackup.Text = "Backup";
            this.buttonRealizaBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRealizaBackup.UseVisualStyleBackColor = true;
            this.buttonRealizaBackup.Click += new System.EventHandler(this.buttonRealizaBackup_Click);
            // 
            // buttonSelecionarArquivoBackup
            // 
            this.buttonSelecionarArquivoBackup.Location = new System.Drawing.Point(348, 27);
            this.buttonSelecionarArquivoBackup.Name = "buttonSelecionarArquivoBackup";
            this.buttonSelecionarArquivoBackup.Size = new System.Drawing.Size(106, 23);
            this.buttonSelecionarArquivoBackup.TabIndex = 0;
            this.buttonSelecionarArquivoBackup.Text = "Selecionar";
            this.buttonSelecionarArquivoBackup.UseVisualStyleBackColor = true;
            this.buttonSelecionarArquivoBackup.Click += new System.EventHandler(this.buttonSelecionarArquivoBackup_Click);
            // 
            // textBoxArquivoBackup
            // 
            this.textBoxArquivoBackup.Location = new System.Drawing.Point(12, 28);
            this.textBoxArquivoBackup.Name = "textBoxArquivoBackup";
            this.textBoxArquivoBackup.Size = new System.Drawing.Size(330, 20);
            this.textBoxArquivoBackup.TabIndex = 2;
            // 
            // sFDialogArquivoBackup
            // 
            this.sFDialogArquivoBackup.DefaultExt = "bak";
            this.sFDialogArquivoBackup.Filter = "Arquivo de backup(*.bak)|*.bak";
            this.sFDialogArquivoBackup.Title = "Salvar Como. . .";
            // 
            // oFDialogArquivoRestauracao
            // 
            this.oFDialogArquivoRestauracao.DefaultExt = "gbk";
            this.oFDialogArquivoRestauracao.FileName = "backup";
            this.oFDialogArquivoRestauracao.Filter = "Arquivo de backup(*.gbk)|*.gbk";
            // 
            // sFDialogCopiaFisica
            // 
            this.sFDialogCopiaFisica.DefaultExt = "fbd";
            this.sFDialogCopiaFisica.Filter = "Arquivo de banco de dados(*.fdb)|*.fdb";
            this.sFDialogCopiaFisica.Title = "Salvar Como. . .";
            // 
            // pgbProgresso
            // 
            this.pgbProgresso.Location = new System.Drawing.Point(12, 169);
            this.pgbProgresso.Name = "pgbProgresso";
            this.pgbProgresso.Size = new System.Drawing.Size(442, 23);
            this.pgbProgresso.Step = 1;
            this.pgbProgresso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbProgresso.TabIndex = 14;
            // 
            // F_UTILITARIOBANCO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 202);
            this.Controls.Add(this.pnlComponentes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "F_UTILITARIOBANCO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Utilitário banco de dados";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_UTILITARIOBANCO_FormClosing);
            this.Load += new System.EventHandler(this.F_UTILITARIOBANCO_Load);
            this.pnlComponentes.ResumeLayout(false);
            this.pnlComponentes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlComponentes;
        private System.Windows.Forms.SaveFileDialog sFDialogArquivoBackup;
        private System.Windows.Forms.OpenFileDialog oFDialogArquivoRestauracao;
        private System.Windows.Forms.SaveFileDialog sFDialogCopiaFisica;
        private System.ComponentModel.BackgroundWorker bgWCopia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRealizaBackup;
        private System.Windows.Forms.Button buttonSelecionarArquivoBackup;
        private System.Windows.Forms.TextBox textBoxArquivoBackup;
        private System.Windows.Forms.Label lblProgresso;
        private System.Windows.Forms.ProgressBar pgbProgresso;
    }
}