using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Conection
{
    static class Program
    {
        public static string AplicacaoChamadora;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (Args.Length > 0)
                AplicacaoChamadora = Args[0];
            
            Application.Run(new F_UTILITARIOBANCO());
        }

    }
}
