
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattleTools
{


    public interface IRequestModel
    {
        KeyValuePair<string, string>[] ToKeyPair();
        string ToJson();
    }

    public class SerializeSiniigaModels
    {
        private const string FORMAT = "\"{0}\":\"{1}\"";
        public static string GetJson(IRequestModel modelo)
        {
            KeyValuePair<string, string>[] values = modelo.ToKeyPair();
            string json = "{";
            for (int index = 0; index < values.Length; index++)
            {
                KeyValuePair<string, string> value = values[index];
                json += string.Format(FORMAT, value.Key, value.Value);
                json += (index < values.Length - 1 ? "," : "}");
            }
            return json;
        }
    }



    class DiasHistoricoComederoRequestModel : IRequestModel
    {
        public int Dias { get; set; }

        public string ToJson()
        {
            return Dias.ToString();
        }

        public KeyValuePair<string, string>[] ToKeyPair()
        {
            return null;
        }
    }

    public class ConsultaCampoRequestModel : IRequestModel
    {
        public string consultaid { get; set; } = "";
        public string nombreConsulta { get; set; } = "";
        public string groupby { get; set; } = "";
        public string orderby { get; set; } = "";
        public string where { get; set; } = "";
        public string from { get; set; } = "";
        public string select { get; set; } = "";

        public string ToJson()
        {
            string st = JsonConvert.SerializeObject(this);
            return st.Replace("\r","\n");
        }

        public KeyValuePair<string, string>[] ToKeyPair()
        {
            throw new NotImplementedException();
        }
    }
}
