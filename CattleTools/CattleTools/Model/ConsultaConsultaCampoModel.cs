using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CattleTools.Model
{
    public class ConsultaConsultaCampoModel
    {
        public string Consulta { get; set; }
        public string Delete { get; set; }
        public List<string> ConsultaCampo { get; set; }
        [JsonIgnore]
        public string ConsultaCampoText
        {
            get
            {
                if (ConsultaCampo == null)
                {
                    return "";
                }
                else
                {
                    string ret = "";
                    foreach (string st in ConsultaCampo)
                    {
                        ret += $"{st}\n";
                    }
                    return ret;
                }
            }
        }
    }

    public class MessageConsultaCampoModel
    {
        public string Message { get; set; }
    }
}
