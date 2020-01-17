using System;
using System.Windows.Forms;
using CustomControls.Fonte.Classes.Log;

namespace CustomControls.Fonte.Classes
{
    public static class Mensagem
    {
        public static void Erro(IWin32Window container, string msg)
        {
            MessageBox.Show(container, msg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logging.Log(true, "Erro: " + msg);
        }

        public static void Erro(IWin32Window container, string msg, string titulo)
        {
            MessageBox.Show(container, msg, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Aviso(IWin32Window container, string msg)
        {
            MessageBox.Show(container, msg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Aviso(IWin32Window container, string msg, string titulo)
        {
            MessageBox.Show(container, msg, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Atencao(IWin32Window container, string msg)
        {
            MessageBox.Show(container, msg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Atencao(IWin32Window container, string msg, string titulo)
        {
            MessageBox.Show(container, msg, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool Pergunta(IWin32Window container, string msg, DialogResult respostaEsperada)
        {
            return MessageBox.Show(container, msg, "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                   respostaEsperada;
        }

        public static bool Pergunta(IWin32Window container, string msg, string titulo, DialogResult respostaEsperada)
        {
            return MessageBox.Show(container, msg, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                   respostaEsperada;
        }

        public static void Excecao(IWin32Window container, string msg, Exception ex)
        {
            MessageBox.Show(container, msg + "Detalhes:\n\n" + ex.Message, "Atenção", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            Logging.Log(true, ex);
        }

        public static void Excecao(IWin32Window container, string msg, string titulo, Exception ex)
        {
            MessageBox.Show(container, msg + "Detalhes" + ex.Message, titulo, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            Logging.Log(true, ex);
        }

        public static void Excecao(IWin32Window container, Exception ex)
        {
            MessageBox.Show(container, ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logging.Log(true, ex);
        }
    }
}