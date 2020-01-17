namespace System.ProtecInfo
{
    using Data;
    using IO;
    using Management;
    using Security.Cryptography;
    using Text;
    using Windows.Forms;

    public static class ProtecInfo
    {
        private static readonly string PATH_REGISTRY = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\prtSys.nfo",
                                       PATH_XMLTEMP = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\prtSys_nfo.xml";

        private const string KEYID = "ID",
                             KEYISVALID = "IsValid",
                             KEYDATAEXPIRACAO = "DTEXP",
                             KEYDATAULTEXEC = "DTULTEXEC",
                             KEYCONTRACHAVE = "CKEY",
                             KEYISDEMO = "IsDemo",
                             STRONGKEY = "2@%*4dX#";

        private static string UniqueID, NomeApp, HDInst;
        private static InfoSys InformacaoRegistro;


        /// <summary>
        /// Retorna o UniqueID do computador de acordo com o drive selecionado
        /// </summary>
        /// <returns>UniqueID</returns>
        private static string GetUniqueID()
        {
            return GetCPUID() + GetHDID();
        }

        /// <summary>
        /// Retorna o HDID do Disco local selecionado.
        /// </summary>
        /// <returns>HDID</returns>
        private static string GetHDID()
        {
            var dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" +
                HDInst + @":""");

            dsk.Get();

            return dsk["VolumeSerialNumber"].ToString();
        }

        /// <summary>
        /// Retorna o CPU ID do computador.
        /// </summary>
        /// <returns>CPU ID</returns>
        private static string GetCPUID()
        {
            var cpuInfo = string.Empty;
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }

            return cpuInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HDInstalado"></param>
        /// <param name="NomeAplicacao"></param>
        public static bool ProtecSystem(string HDInstalado, string NomeAplicacao)
        {
            NomeApp = NomeAplicacao;
            HDInst = HDInstalado;

            VerificaArquivoRegistro();

            if (InformacaoRegistro.IsValid)
            {
                // Consistencias de sistema válido
                ConsisteNormal();
                return InformacaoRegistro.IsValid;
            }

            // Consistencias de sistema Demo
            return ConsisteDemo();
        }

        private static bool ConsisteDemo()
        {
            if (InformacaoRegistro.DataUltimaExecucao > DateTime.Now)
            {
                MessageBox.Show("Data da ultima execução maior que a data atual, Verifique a data do seu computador", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error, 
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                
                return false;
            }

            if (InformacaoRegistro.DataExpiracao < DateTime.Now)
            {
                FrmValidacao frm = new FrmValidacao();
                frm.ShowDialog();
                return false;
            }

            return true;
        }

        private static void ConsisteNormal()
        {
            Console.WriteLine("Normal");
        }

        private static void VerificaArquivoRegistro()
        {
            var fileInfoReg = new FileInfo(PATH_REGISTRY);

            if (!fileInfoReg.Exists)
                CriaArquivoRegistro();

            PreencheInfoSys();
        }

        private static void CriaArquivoRegistro()
        {
            UniqueID = GetUniqueID();

            // Gera o nome do dataset como ProtecSys
            string strAux = Criptografar("<ProtecSys>");
            CriaEscreveArquivoRegistro(strAux);
            
            // Gera o nome da tabela
            strAux = Criptografar("<" + NomeApp + ">");
            CriaEscreveArquivoRegistro(strAux);
            
            // Gera a informação do UniqueID
            strAux = Criptografar("<" + KEYID + ">" + UniqueID + "</" + KEYID + ">");
            CriaEscreveArquivoRegistro(strAux);

            // Gera a informação da contrachave
            strAux = Criptografar("<" + KEYCONTRACHAVE + "></" + KEYCONTRACHAVE + ">");
            CriaEscreveArquivoRegistro(strAux);

            // Gera a informação de sistema válidado
            strAux = Criptografar("<" + KEYISVALID + ">" + bool.FalseString + "</" + KEYISVALID + ">");
            CriaEscreveArquivoRegistro(strAux);

            // Gera a informação de data de ultima execucao
            strAux = Criptografar("<" + KEYDATAULTEXEC + ">" + DateTime.Now + "</" + KEYDATAULTEXEC + ">");
            CriaEscreveArquivoRegistro(strAux);

            // Como é a geração inicial, gera uma data de expiração DEMO de 30 Dias
            strAux = Criptografar("<" + KEYISDEMO + ">" + bool.TrueString + "</" + KEYISDEMO + ">");
            CriaEscreveArquivoRegistro(strAux);

            // Gera a informação de data de expiracao
            strAux = Criptografar("<" + KEYDATAEXPIRACAO + ">" + DateTime.Now.Add(TimeSpan.FromDays(30)) + "</" + KEYDATAEXPIRACAO + ">");
            CriaEscreveArquivoRegistro(strAux);

            // Fecha chave
            strAux = Criptografar("</" + NomeApp + ">");
            CriaEscreveArquivoRegistro(strAux);

            // Fecha chave
            strAux = Criptografar("</ProtecSys>");
            CriaEscreveArquivoRegistro(strAux);
        }

        private static void CriaEscreveArquivoRegistro(string strTxt)
        {
            StreamWriter W_Sw_Log;

            if (File.Exists(PATH_REGISTRY))
            {
                using (W_Sw_Log = File.AppendText(PATH_REGISTRY))
                    W_Sw_Log.WriteLine(strTxt);
            }
            else
            {
                using (W_Sw_Log = new StreamWriter(PATH_REGISTRY))
                    W_Sw_Log.WriteLine(strTxt);
            }

            W_Sw_Log.Close();
            W_Sw_Log.Dispose();
        }

        private static void CriaEscreveArquivoTemp(string strTxt)
        {
            StreamWriter W_Sw_Log;

            if (File.Exists(PATH_XMLTEMP))
            {
                using (W_Sw_Log = File.AppendText(PATH_XMLTEMP))
                    W_Sw_Log.WriteLine(strTxt);
            }
            else
            {
                using (W_Sw_Log = new StreamWriter(PATH_XMLTEMP))
                    W_Sw_Log.WriteLine(strTxt);
            }

            W_Sw_Log.Close();
            W_Sw_Log.Dispose();
        }

        private static void PreencheInfoSys()
        {
            //Cria o DataSet
            var ds = new DataSet();

            using (var sr = new StreamReader(PATH_REGISTRY))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                    CriaEscreveArquivoTemp(Descriptografar(line));
            }

            //Carrega os dados
            ds.ReadXml(PATH_XMLTEMP);

            if (ds.Tables.Contains(NomeApp))
            {
                DataTable table = ds.Tables[NomeApp];

                DataRow row = table.Rows[0];

                InformacaoRegistro = new InfoSys
                                         {
                                             Aplicacao = NomeApp,
                                             ID = row[KEYID].ToString(),
                                             DataExpiracao = Convert.ToDateTime(row[KEYDATAEXPIRACAO]),
                                             IsValid = Convert.ToBoolean(row[KEYISVALID]),
                                             ContraChave = row[KEYCONTRACHAVE].ToString(),
                                             IsDemo = Convert.ToBoolean(row[KEYISDEMO]),
                                             DataUltimaExecucao = Convert.ToDateTime(row[KEYDATAULTEXEC]),
                                         };

            }
            File.Delete(PATH_XMLTEMP);
        }

        #region Criptografia
        private static byte[] chave = { };
        private static readonly byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };

        public static string Criptografar(string valor)
        {
            var des = new DESCryptoServiceProvider();
            var ms = new MemoryStream();

            byte[] input = Encoding.UTF8.GetBytes(valor);
            chave = Encoding.UTF8.GetBytes(STRONGKEY);
            var cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Descriptografar(string valor)
        {
            var des = new DESCryptoServiceProvider();
            var ms = new MemoryStream();

            byte[] input = Convert.FromBase64String(valor.Replace(" ", "+"));

            chave = Encoding.UTF8.GetBytes(STRONGKEY);

            var cs = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.FlushFinalBlock();

            return Encoding.UTF8.GetString(ms.ToArray());
        }

        #endregion
    }

    class InfoSys
    {
        public string Aplicacao { get; set; }
        public string ID { get; set; }
        public bool IsValid { get; set; }
        public bool IsDemo { get; set; }
        public DateTime DataExpiracao { get; set; }
        public DateTime DataUltimaExecucao { get; set; }
        public string ContraChave { get; set; }
    }
}
