using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using CustomControls.Fonte.Enum;

namespace CustomControls.Fonte.Classes
{
    /// <sumary>
    ///
    /// </sumary>
    public class Imagem
    {
        /// <summary>
        /// Redimensiona uma determinada imagem. OBS.: Este método altera as proporções da imagem.
        /// </summary>
        /// <param name="imgOrigem">Imagem original</param>
        /// <param name="szTamanhoDestino">Tamanho de destino</param>
        /// <returns></returns>
        public Image RedimensionaImagem(Image imgOrigem, Size szTamanhoDestino)
        {
            Bitmap lbmpImageResultado = new Bitmap(szTamanhoDestino.Width, szTamanhoDestino.Height,
                                                   PixelFormat.Format32bppPArgb);

            lbmpImageResultado.SetResolution(imgOrigem.HorizontalResolution, imgOrigem.VerticalResolution);

            using (Graphics lgrfResultado = Graphics.FromImage(lbmpImageResultado))
            {
                lgrfResultado.InterpolationMode = InterpolationMode.HighQualityBicubic;
                lgrfResultado.DrawImage(imgOrigem, 0, 0, szTamanhoDestino.Width, szTamanhoDestino.Height);
            }

            return lbmpImageResultado;
        }

        /// <summary>
        /// Realiza a fusão de duas imagens criando apenas uma 
        /// </summary>
        /// <param name="imgImagemGrande">Imagem maior que servirá de container para a segunda imagem</param>
        /// <param name="imgImagemPequena">Imagem menor que irá ser embutida na imagem container</param>
        /// <param name="enmPosicao">Posicao onde a imagem pequena será colocada</param>
        /// <param name="szTamanhoRedimensionamento">Tamanho de redimensionamento. Seu valor determina se a imagem 
        /// final terá as mesmas dimensões da imagem maior (valor nulo) ou se será um redimensionamento</param>
        /// <param name="intMargemInferior">Margem inferior</param>
        /// <param name="intMargemSuperior">Margem superior</param>
        /// <param name="intMargemDireita">Margem direita</param>
        /// <param name="intMargemEsquerda">Margem esquerda</param>
        /// <returns></returns>
        public Image FundirImagens(Image imgImagemGrande, Image imgImagemPequena,
                                   Size? szTamanhoRedimensionamento, EnumImagem.PosicaoAncoragem enmPosicao,
                                   int intMargemInferior, int intMargemSuperior, int intMargemDireita,
                                   int intMargemEsquerda)
        {
            int lintPosicaoX = 0,
                lintPosicaoY = 0;

            Size lszRedimensionamento = szTamanhoRedimensionamento ?? imgImagemGrande.Size;

            // Calcula a posição considerando o tamanho da imagem considerando as margens.
            switch (enmPosicao)
            {
                case EnumImagem.PosicaoAncoragem.InferiorDireita:
                    {
                        lintPosicaoX = lszRedimensionamento.Width - (imgImagemPequena.Width + intMargemDireita)
                            - intMargemEsquerda;
                        lintPosicaoY = lszRedimensionamento.Height - (imgImagemPequena.Height + intMargemInferior)
                            - intMargemSuperior;
                        break;
                    }
                case EnumImagem.PosicaoAncoragem.Centro:
                    {
                        lintPosicaoX = (lszRedimensionamento.Width - (imgImagemPequena.Width + intMargemDireita)) / 2
                            - intMargemEsquerda;
                        lintPosicaoY = (lszRedimensionamento.Height - (imgImagemPequena.Height + intMargemInferior)) / 2
                            - intMargemSuperior;
                        break;
                    }
                case EnumImagem.PosicaoAncoragem.SuperiorEsquerda:
                    {
                        lintPosicaoX = 0;
                        lintPosicaoY = 0;
                        break;
                    }
            }

            // Se o tamanho de redimensionamento não for informado, utiliza a imagem orignal
            Image lbmpResultado = szTamanhoRedimensionamento == null
                                      ? imgImagemGrande
                                      : RedimensionaImagem(imgImagemGrande, lszRedimensionamento);

            using (Graphics lgrfResultado = Graphics.FromImage(lbmpResultado))
            {
                lgrfResultado.InterpolationMode = InterpolationMode.HighQualityBicubic;
                lgrfResultado.DrawImage(imgImagemPequena, lintPosicaoX, lintPosicaoY, imgImagemPequena.Width,
                                        imgImagemPequena.Height);
            }

            GC.Collect();
            return lbmpResultado;
        }
    }
}