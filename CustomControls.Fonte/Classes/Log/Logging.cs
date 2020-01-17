using System;
using System.IO;
using System.Windows.Forms;

namespace CustomControls.Fonte.Classes.Log
{
    public static class Logging
    {
        public static string PathArquivo = String.Empty;

        public static bool Aberto
        {
            get { return File.Exists(PathArquivo); }
        }

        public static void GetArquivoLog()
        {
            PathArquivo = Path.GetDirectoryName(Application.ExecutablePath) + @"\AppLog.log";
        }

        private static void InitLog(bool append)
        {
            GetArquivoLog();

            if (!Aberto)
                CriaArquivoLog(append);
        }

        public static bool CriaArquivoLog(bool append)
        {
            try
            {
                GetArquivoLog();

                if (!File.Exists(PathArquivo))
                {
                    StreamWriter logFile;
                    using (logFile = new StreamWriter(PathArquivo, append))
                    {
                        logFile.WriteLine(DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss" + " - Log Iniciado."));
                    }
                    logFile.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Log(bool append, string texto)
        {
            InitLog(append);

            using (var writer = new StreamWriter(PathArquivo, true))
            {
                writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss") + " - " + texto);
                writer.Close();
            }
        }

        public static void Log(bool append, Exception ex)
        {
            InitLog(append);
            Log(append, "Ocorreu uma Exceção: " + ex.Message);
        }

        public static void Log(bool append, EventArgs e, object sender)
        {
            InitLog(append);
            Log(append, "Evento: " + e + " Em: " + sender);
        }
    }
}