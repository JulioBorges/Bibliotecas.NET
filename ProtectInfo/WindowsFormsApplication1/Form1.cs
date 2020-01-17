using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    using System.Management;
    using System.ProtecInfo;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ProtecInfo.ProtecSystem("C", "AplicacaoTeste"))
                MessageBox.Show("OK");
            else
            {
                MessageBox.Show("Invalido");
                Close();
            }
        }
    }
}
