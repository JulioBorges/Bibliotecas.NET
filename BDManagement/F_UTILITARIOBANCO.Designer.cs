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
            this.panelFirebird = new System.Windows.Forms.Panel();
            this.tabControlBancoDados = new System.Windows.Forms.TabControl();
            this.tabPageBackup = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxInfoBackup = new System.Windows.Forms.ListBox();
            this.buttonRealizaBackup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxArquivoBackup = new System.Windows.Forms.TextBox();
            this.buttonSelecionarArquivoBackup = new System.Windows.Forms.Button();
            this.tabPageRestauracao = new System.Windows.Forms.TabPage();
            this.listBoxInfoRestauracao = new System.Windows.Forms.ListBox();
            this.buttonRealizaRestauração = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxArquivoRestauracao = new System.Windows.Forms.TextBox();
            this.buttonSelecionarArquivoRestauracao = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageReorganizacao = new System.Windows.Forms.TabPage();
            this.buttonRealizaReorganizacao = new System.Windows.Forms.Button();
            this.listBoxInfoReorganizacao = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageCopiaFisica = new System.Windows.Forms.TabPage();
            this.labelProgressoCopia = new System.Windows.Forms.Label();
            this.buttonCancelarCopia = new System.Windows.Forms.Button();
            this.progressBarProgresso = new System.Windows.Forms.ProgressBar();
            this.buttonCopiaFisica = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxArquivoCopiaFisica = new System.Windows.Forms.TextBox();
            this.buttonSelecionaCopiaFisica = new System.Windows.Forms.Button();
            this.sFDialogArquivoBackup = new System.Windows.Forms.SaveFileDialog();
            this.oFDialogArquivoRestauracao = new System.Windows.Forms.OpenFileDialog();
            this.sFDialogCopiaFisica = new System.Windows.Forms.SaveFileDialog();
            this.bgWCopia = new System.ComponentModel.BackgroundWorker();
            this.panelFirebird.SuspendLayout();
            this.tabControlBancoDados.SuspendLayout();
            this.tabPageBackup.SuspendLayout();
            this.tabPageRestauracao.SuspendLayout();
            this.tabPageReorganizacao.SuspendLayout();
            this.tabPageCopiaFisica.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFirebird
            // 
            this.panelFirebird.Controls.Add(this.tabControlBancoDados);
            this.panelFirebird.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFirebird.Location = new System.Drawing.Point(0, 0);
            this.panelFirebird.Name = "panelFirebird";
            this.panelFirebird.Size = new System.Drawing.Size(467, 291);
            this.panelFirebird.TabIndex = 0;
            // 
            // tabControlBancoDados
            // 
            this.tabControlBancoDados.Controls.Add(this.tabPageBackup);
            this.tabControlBancoDados.Controls.Add(this.tabPageRestauracao);
            this.tabControlBancoDados.Controls.Add(this.tabPageReorganizacao);
            this.tabControlBancoDados.Controls.Add(this.tabPageCopiaFisica);
            this.tabControlBancoDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlBancoDados.Location = new System.Drawing.Point(0, 0);
            this.tabControlBancoDados.Name = "tabControlBancoDados";
            this.tabControlBancoDados.SelectedIndex = 0;
            this.tabControlBancoDados.Size = new System.Drawing.Size(467, 291);
            this.tabControlBancoDados.TabIndex = 0;
            // 
            // tabPageBackup
            // 
            this.tabPageBackup.Controls.Add(this.label2);
            this.tabPageBackup.Controls.Add(this.listBoxInfoBackup);
            this.tabPageBackup.Controls.Add(this.buttonRealizaBackup);
            this.tabPageBackup.Controls.Add(this.label1);
            this.tabPageBackup.Controls.Add(this.textBoxArquivoBackup);
            this.tabPageBackup.Controls.Add(this.buttonSelecionarArquivoBackup);
            this.tabPageBackup.Location = new System.Drawing.Point(4, 22);
            this.tabPageBackup.Name = "tabPageBackup";
            this.tabPageBackup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBackup.Size = new System.Drawing.Size(459, 265);
            this.tabPageBackup.TabIndex = 0;
            this.tabPageBackup.Text = "Backup";
            this.tabPageBackup.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(8, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 82);
            this.label2.TabIndex = 11;
            this.label2.Text = "Atenção\r\nRecomenda-se que os arquivos de backup sejam salvos em mídias externas, " +
                "para preservar e garantir a integridade dos dados da aplicação.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxInfoBackup
            // 
            this.listBoxInfoBackup.FormattingEnabled = true;
            this.listBoxInfoBackup.Location = new System.Drawing.Point(8, 157);
            this.listBoxInfoBackup.Name = "listBoxInfoBackup";
            this.listBoxInfoBackup.Size = new System.Drawing.Size(443, 95);
            this.listBoxInfoBackup.TabIndex = 6;
            // 
            // buttonRealizaBackup
            // 
            this.buttonRealizaBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRealizaBackup.Image = ((System.Drawing.Image)(resources.GetObject("buttonRealizaBackup.Image")));
            this.buttonRealizaBackup.Location = new System.Drawing.Point(344, 67);
            this.buttonRealizaBackup.Name = "buttonRealizaBackup";
            this.buttonRealizaBackup.Size = new System.Drawing.Size(107, 75);
            this.buttonRealizaBackup.TabIndex = 5;
            this.buttonRealizaBackup.Text = "Backup";
            this.buttonRealizaBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRealizaBackup.UseVisualStyleBackColor = true;
            this.buttonRealizaBackup.Click += new System.EventHandler(this.buttonRealizaBackup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Arquivo de backup";
            // 
            // textBoxArquivoBackup
            // 
            this.textBoxArquivoBackup.Location = new System.Drawing.Point(8, 32);
            this.textBoxArquivoBackup.Name = "textBoxArquivoBackup";
            this.textBoxArquivoBackup.Size = new System.Drawing.Size(330, 20);
            this.textBoxArquivoBackup.TabIndex = 2;
            // 
            // buttonSelecionarArquivoBackup
            // 
            this.buttonSelecionarArquivoBackup.Location = new System.Drawing.Point(344, 31);
            this.buttonSelecionarArquivoBackup.Name = "buttonSelecionarArquivoBackup";
            this.buttonSelecionarArquivoBackup.Size = new System.Drawing.Size(107, 23);
            this.buttonSelecionarArquivoBackup.TabIndex = 0;
            this.buttonSelecionarArquivoBackup.Text = "Selecionar";
            this.buttonSelecionarArquivoBackup.UseVisualStyleBackColor = true;
            this.buttonSelecionarArquivoBackup.Click += new System.EventHandler(this.buttonSelecionarArquivoBackup_Click);
            // 
            // tabPageRestauracao
            // 
            this.tabPageRestauracao.Controls.Add(this.listBoxInfoRestauracao);
            this.tabPageRestauracao.Controls.Add(this.buttonRealizaRestauração);
            this.tabPageRestauracao.Controls.Add(this.label4);
            this.tabPageRestauracao.Controls.Add(this.textBoxArquivoRestauracao);
            this.tabPageRestauracao.Controls.Add(this.buttonSelecionarArquivoRestauracao);
            this.tabPageRestauracao.Controls.Add(this.label5);
            this.tabPageRestauracao.Location = new System.Drawing.Point(4, 22);
            this.tabPageRestauracao.Name = "tabPageRestauracao";
            this.tabPageRestauracao.Size = new System.Drawing.Size(459, 265);
            this.tabPageRestauracao.TabIndex = 2;
            this.tabPageRestauracao.Text = "Restauração";
            this.tabPageRestauracao.UseVisualStyleBackColor = true;
            // 
            // listBoxInfoRestauracao
            // 
            this.listBoxInfoRestauracao.FormattingEnabled = true;
            this.listBoxInfoRestauracao.Location = new System.Drawing.Point(8, 157);
            this.listBoxInfoRestauracao.Name = "listBoxInfoRestauracao";
            this.listBoxInfoRestauracao.Size = new System.Drawing.Size(443, 95);
            this.listBoxInfoRestauracao.TabIndex = 12;
            // 
            // buttonRealizaRestauração
            // 
            this.buttonRealizaRestauração.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRealizaRestauração.Image = ((System.Drawing.Image)(resources.GetObject("buttonRealizaRestauração.Image")));
            this.buttonRealizaRestauração.Location = new System.Drawing.Point(344, 67);
            this.buttonRealizaRestauração.Name = "buttonRealizaRestauração";
            this.buttonRealizaRestauração.Size = new System.Drawing.Size(107, 75);
            this.buttonRealizaRestauração.TabIndex = 11;
            this.buttonRealizaRestauração.Text = "Resturação";
            this.buttonRealizaRestauração.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRealizaRestauração.UseVisualStyleBackColor = true;
            this.buttonRealizaRestauração.Click += new System.EventHandler(this.buttonRealizaRestauração_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Arquivo de backup";
            // 
            // textBoxArquivoRestauracao
            // 
            this.textBoxArquivoRestauracao.Location = new System.Drawing.Point(8, 32);
            this.textBoxArquivoRestauracao.Name = "textBoxArquivoRestauracao";
            this.textBoxArquivoRestauracao.Size = new System.Drawing.Size(330, 20);
            this.textBoxArquivoRestauracao.TabIndex = 9;
            // 
            // buttonSelecionarArquivoRestauracao
            // 
            this.buttonSelecionarArquivoRestauracao.Location = new System.Drawing.Point(344, 31);
            this.buttonSelecionarArquivoRestauracao.Name = "buttonSelecionarArquivoRestauracao";
            this.buttonSelecionarArquivoRestauracao.Size = new System.Drawing.Size(107, 23);
            this.buttonSelecionarArquivoRestauracao.TabIndex = 8;
            this.buttonSelecionarArquivoRestauracao.Text = "Selecionar";
            this.buttonSelecionarArquivoRestauracao.UseVisualStyleBackColor = true;
            this.buttonSelecionarArquivoRestauracao.Click += new System.EventHandler(this.buttonSelecionarArquivoRestauracao_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(10, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(330, 78);
            this.label5.TabIndex = 13;
            this.label5.Text = "Atenção\r\nA restauração de banco de dados, restaura todos os dados por meio de um " +
                "arquivo com extenção *.gbk. Muito cuidado ! Este processo poderá causar perca de" +
                " dados.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageReorganizacao
            // 
            this.tabPageReorganizacao.Controls.Add(this.buttonRealizaReorganizacao);
            this.tabPageReorganizacao.Controls.Add(this.listBoxInfoReorganizacao);
            this.tabPageReorganizacao.Controls.Add(this.label6);
            this.tabPageReorganizacao.Location = new System.Drawing.Point(4, 22);
            this.tabPageReorganizacao.Name = "tabPageReorganizacao";
            this.tabPageReorganizacao.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReorganizacao.Size = new System.Drawing.Size(459, 265);
            this.tabPageReorganizacao.TabIndex = 1;
            this.tabPageReorganizacao.Text = "Reorganização Banco";
            this.tabPageReorganizacao.UseVisualStyleBackColor = true;
            // 
            // buttonRealizaReorganizacao
            // 
            this.buttonRealizaReorganizacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRealizaReorganizacao.Image = ((System.Drawing.Image)(resources.GetObject("buttonRealizaReorganizacao.Image")));
            this.buttonRealizaReorganizacao.Location = new System.Drawing.Point(336, 35);
            this.buttonRealizaReorganizacao.Name = "buttonRealizaReorganizacao";
            this.buttonRealizaReorganizacao.Size = new System.Drawing.Size(107, 75);
            this.buttonRealizaReorganizacao.TabIndex = 12;
            this.buttonRealizaReorganizacao.Text = "Reorganização";
            this.buttonRealizaReorganizacao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRealizaReorganizacao.UseVisualStyleBackColor = true;
            this.buttonRealizaReorganizacao.Click += new System.EventHandler(this.buttonReorganizacao_Click);
            // 
            // listBoxInfoReorganizacao
            // 
            this.listBoxInfoReorganizacao.FormattingEnabled = true;
            this.listBoxInfoReorganizacao.Location = new System.Drawing.Point(8, 138);
            this.listBoxInfoReorganizacao.Name = "listBoxInfoReorganizacao";
            this.listBoxInfoReorganizacao.Size = new System.Drawing.Size(443, 121);
            this.listBoxInfoReorganizacao.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(8, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(322, 116);
            this.label6.TabIndex = 9;
            this.label6.Text = "A reorganização é um procedimento de segurança que organiza os dados, diminuindo " +
                "o tamanho do banco de dados e garantindo uma execução segura da aplicação";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageCopiaFisica
            // 
            this.tabPageCopiaFisica.Controls.Add(this.labelProgressoCopia);
            this.tabPageCopiaFisica.Controls.Add(this.buttonCancelarCopia);
            this.tabPageCopiaFisica.Controls.Add(this.progressBarProgresso);
            this.tabPageCopiaFisica.Controls.Add(this.buttonCopiaFisica);
            this.tabPageCopiaFisica.Controls.Add(this.label8);
            this.tabPageCopiaFisica.Controls.Add(this.label7);
            this.tabPageCopiaFisica.Controls.Add(this.textBoxArquivoCopiaFisica);
            this.tabPageCopiaFisica.Controls.Add(this.buttonSelecionaCopiaFisica);
            this.tabPageCopiaFisica.Location = new System.Drawing.Point(4, 22);
            this.tabPageCopiaFisica.Name = "tabPageCopiaFisica";
            this.tabPageCopiaFisica.Size = new System.Drawing.Size(459, 265);
            this.tabPageCopiaFisica.TabIndex = 3;
            this.tabPageCopiaFisica.Text = "Cópia Física";
            this.tabPageCopiaFisica.UseVisualStyleBackColor = true;
            // 
            // labelProgressoCopia
            // 
            this.labelProgressoCopia.AutoSize = true;
            this.labelProgressoCopia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgressoCopia.Location = new System.Drawing.Point(19, 219);
            this.labelProgressoCopia.Name = "labelProgressoCopia";
            this.labelProgressoCopia.Size = new System.Drawing.Size(0, 13);
            this.labelProgressoCopia.TabIndex = 16;
            // 
            // buttonCancelarCopia
            // 
            this.buttonCancelarCopia.Enabled = false;
            this.buttonCancelarCopia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelarCopia.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancelarCopia.Image")));
            this.buttonCancelarCopia.Location = new System.Drawing.Point(298, 141);
            this.buttonCancelarCopia.Name = "buttonCancelarCopia";
            this.buttonCancelarCopia.Size = new System.Drawing.Size(138, 65);
            this.buttonCancelarCopia.TabIndex = 15;
            this.buttonCancelarCopia.Text = "Cancelar cópia";
            this.buttonCancelarCopia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCancelarCopia.UseVisualStyleBackColor = true;
            this.buttonCancelarCopia.Click += new System.EventHandler(this.buttonCancelarCopia_Click);
            // 
            // progressBarProgresso
            // 
            this.progressBarProgresso.Location = new System.Drawing.Point(22, 235);
            this.progressBarProgresso.Name = "progressBarProgresso";
            this.progressBarProgresso.Size = new System.Drawing.Size(414, 22);
            this.progressBarProgresso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarProgresso.TabIndex = 14;
            // 
            // buttonCopiaFisica
            // 
            this.buttonCopiaFisica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCopiaFisica.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopiaFisica.Image")));
            this.buttonCopiaFisica.Location = new System.Drawing.Point(22, 140);
            this.buttonCopiaFisica.Name = "buttonCopiaFisica";
            this.buttonCopiaFisica.Size = new System.Drawing.Size(270, 66);
            this.buttonCopiaFisica.TabIndex = 13;
            this.buttonCopiaFisica.Text = "Realizar Cópia Física";
            this.buttonCopiaFisica.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCopiaFisica.UseVisualStyleBackColor = true;
            this.buttonCopiaFisica.Click += new System.EventHandler(this.buttonCopiaFisica_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(8, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(443, 62);
            this.label8.TabIndex = 10;
            this.label8.Text = "Atenção\r\nRecomenda-se que os arquivos de cópia física sejam salvos em mídias exte" +
                "rnas, para preservar e garantir a integridade dos dados da aplicação.";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(8, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(443, 45);
            this.label7.TabIndex = 10;
            this.label7.Text = "A cópia física é um procedimento que realiza uma cópia exata do banco de dados do" +
                " sistema, garantindo um maior controle e segurança da aplicação.";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxArquivoCopiaFisica
            // 
            this.textBoxArquivoCopiaFisica.Location = new System.Drawing.Point(8, 114);
            this.textBoxArquivoCopiaFisica.Name = "textBoxArquivoCopiaFisica";
            this.textBoxArquivoCopiaFisica.Size = new System.Drawing.Size(330, 20);
            this.textBoxArquivoCopiaFisica.TabIndex = 4;
            // 
            // buttonSelecionaCopiaFisica
            // 
            this.buttonSelecionaCopiaFisica.Location = new System.Drawing.Point(344, 112);
            this.buttonSelecionaCopiaFisica.Name = "buttonSelecionaCopiaFisica";
            this.buttonSelecionaCopiaFisica.Size = new System.Drawing.Size(107, 23);
            this.buttonSelecionaCopiaFisica.TabIndex = 3;
            this.buttonSelecionaCopiaFisica.Text = "Selecionar";
            this.buttonSelecionaCopiaFisica.UseVisualStyleBackColor = true;
            this.buttonSelecionaCopiaFisica.Click += new System.EventHandler(this.buttonSelecionaCopiaFisica_Click);
            // 
            // sFDialogArquivoBackup
            // 
            this.sFDialogArquivoBackup.DefaultExt = "gbk";
            this.sFDialogArquivoBackup.Filter = "Arquivo de backup(*.gbk)|*.gbk";
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
            // bgWCopia
            // 
            this.bgWCopia.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWCopia_DoWork);
            this.bgWCopia.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWCopia_RunWorkerCompleted);
            // 
            // F_UTILITARIOBANCO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 291);
            this.Controls.Add(this.panelFirebird);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "F_UTILITARIOBANCO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Utilitário banco de dados";
            this.Load += new System.EventHandler(this.F_UTILITARIOBANCO_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_UTILITARIOBANCO_FormClosing);
            this.panelFirebird.ResumeLayout(false);
            this.tabControlBancoDados.ResumeLayout(false);
            this.tabPageBackup.ResumeLayout(false);
            this.tabPageBackup.PerformLayout();
            this.tabPageRestauracao.ResumeLayout(false);
            this.tabPageRestauracao.PerformLayout();
            this.tabPageReorganizacao.ResumeLayout(false);
            this.tabPageCopiaFisica.ResumeLayout(false);
            this.tabPageCopiaFisica.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFirebird;
        private System.Windows.Forms.TabControl tabControlBancoDados;
        private System.Windows.Forms.TabPage tabPageBackup;
        private System.Windows.Forms.TabPage tabPageReorganizacao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxArquivoBackup;
        private System.Windows.Forms.Button buttonSelecionarArquivoBackup;
        private System.Windows.Forms.Button buttonRealizaBackup;
        private System.Windows.Forms.ListBox listBoxInfoBackup;
        private System.Windows.Forms.TabPage tabPageRestauracao;
        private System.Windows.Forms.TabPage tabPageCopiaFisica;
        private System.Windows.Forms.SaveFileDialog sFDialogArquivoBackup;
        private System.Windows.Forms.ListBox listBoxInfoRestauracao;
        private System.Windows.Forms.Button buttonRealizaRestauração;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxArquivoRestauracao;
        private System.Windows.Forms.Button buttonSelecionarArquivoRestauracao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog oFDialogArquivoRestauracao;
        private System.Windows.Forms.ListBox listBoxInfoReorganizacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonRealizaReorganizacao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxArquivoCopiaFisica;
        private System.Windows.Forms.Button buttonSelecionaCopiaFisica;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCopiaFisica;
        private System.Windows.Forms.SaveFileDialog sFDialogCopiaFisica;
        private System.Windows.Forms.ProgressBar progressBarProgresso;
        private System.ComponentModel.BackgroundWorker bgWCopia;
        private System.Windows.Forms.Button buttonCancelarCopia;
        private System.Windows.Forms.Label labelProgressoCopia;
    }
}