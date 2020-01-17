using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CustomControls.Fonte.Interface;

namespace CustomControls.Fonte.Classes
{
    /// <summary>
    ///   ***********************************************///
    ///   @criado por: Julio Cezar Borges                ///
    ///   @data: 26/09/2011                              ///
    ///   @motivo: Desenvolvimento do projeto            ///
    ///   ***********************************************///
    ///   @modificado por:                               ///
    ///   @data:                                         ///
    ///   @motivo:                                       ///
    ///   ***********************************************///
    /// </summary>
    public static class Funcoes
    {
        public static object ConverteArrayByteParaObjeto(byte[] byteArray)
        {
            var binFormatter = new BinaryFormatter();
            var memStream = new MemoryStream(byteArray);
            try
            {
                memStream.Position = 0;

                return binFormatter.Deserialize(memStream);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu uma exceção durante a conversão: " + ex);
            }
        }

        public static Image ConverteObjetoParaImagem(object obj)
        {
            var byteArrayIn = (byte[]) obj;
            var ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static bool VerificaExisteForm(Form mdiParent, Form form)
        {
            return mdiParent.MdiChildren.Any(item => item.ToString() == form.ToString());
        }

        public static void HabilitaBotoesPesquisa(ToolStripButton[] botoesBrowse, ToolStripButton[] botoesEdit,
                                               bool enabled)
        {
            foreach (var tsb in botoesBrowse)
            {
                tsb.Enabled = enabled;
            }
            foreach (var tsb2 in botoesEdit)
            {
                tsb2.Enabled = !enabled;
            }
        }

        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            var expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            //define a expressão regulara para validar o email
            string textoValidar = enderecoEmail;
            if (textoValidar == null) return false;

            // testa o email com a expressão
            if (expressaoRegex.IsMatch(textoValidar))
                // o email é valido
                return true;
            // o email é inválido
            return false;
        }

        public static IEnumerable<object[]> ListaDeDados(this IDataReader source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            while (source.Read())
            {
                var row = new Object[source.FieldCount];
                source.GetValues(row);
                yield return row;
            }
        }

        public static bool ConverteImagemEmArquivo(object obj, string caminho, int width, int height)
        {
            if (obj != DBNull.Value)
            {
                Image img = Image.FromStream(new MemoryStream((byte[]) obj));

                if ((width != 0) && (height != 0))
                    img = ResizeImage(img, new Size(width, height));

                img.Save(caminho);
                return true;
            }
            
            return false;
        }

        private static Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercentW = size.Width / (float)sourceWidth;
            float nPercentH = size.Height/(float) sourceHeight;

            float nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int) (sourceWidth*nPercent);
            var destHeight = (int) (sourceHeight*nPercent);

            var b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }

        public static bool InDesign()
        {
            return File.Exists("devenv.exe");
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool InternetIsOn()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }

        public static void BindingControles(this Control ctrlControle)
        {
            foreach (Control ctrlContrl in ctrlControle.Controls)
            {
                // Se o controle possui mais de um controle, 
                // significa que ele pode ser um container
                if (ctrlContrl is IData)
                    ctrlContrl.BindarControle(); 
                else if (ctrlContrl.Controls.Count > 0)
                    ctrlContrl.BindingControles();
            }
        }

        //public static void BindingControlsUserControl(this UserControl userControl, BindingSource bds)
        //{
        //    IEnumerable<IData> controles = userControl.Controls.OfType<IData>().ToList();

        //    foreach (var controle in controles)
        //    {
        //        ((Control)controle).BindarControle(bds, controle.Propriedade, controle.Campo);
        //    }
        //}

        //public static void BindingControlsTabPage(this TabPage Frm, BindingSource bds)
        //{
        //    IEnumerable<IData> controles = Frm.Controls.OfType<IData>().ToList();

        //    foreach (var controle in controles)
        //    {
        //        ((Control)controle).BindarControle(bds, controle.Propriedade, controle.Campo);
        //    }
        //}

        public static void BindarControle(this Control controle)
        {
            try
            {
                if (!(controle is IData))
                    return;
                var lstrPropriedade = (controle as IData).Propriedade;
                var lbdsFonte = (controle as IData).FonteDados;
                var lstrcampo = (controle as IData).Campo;
                //if (controle is DateTimePicker || controle is ComboBox || controle is PictureBox)
                //    controle.DataBindings.Add(lstrPropriedade, lbdsFonte, lstrcampo, true, DataSourceUpdateMode.OnPropertyChanged);
                //else
                    controle.DataBindings.Add(lstrPropriedade, lbdsFonte, lstrcampo, true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados na tela.\nDetalhes: " + ex.Message);
            }
        }
    }
}