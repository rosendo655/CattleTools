using CattleTools.Model;
using Newtonsoft.Json;
using Plugin.Clipboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CattleTools
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            this.BindingContext = new ConsultaCampoRequestModel();
		}

        public async  void click(object sender, EventArgs e)
        {
            HttpResponseMessage req;
            string response="";

            try
            {
                req = HttpClientCustom.MakeRequest(HttpMethod.Post, new Uri("http://"+ip.Text,UriKind.Absolute), this.BindingContext as IRequestModel, null);
                response = await req.Content.ReadAsStringAsync();
                resConsulta.Text = "";
                resConsultaCampo.Text = "";
                var m = JsonConvert.DeserializeObject<ConsultaConsultaCampoModel>(response);
                resConsulta.Text = m.Consulta;
                foreach(string st in m.ConsultaCampo)
                {
                    resConsultaCampo.Text += $"{st}\n";
                }
                CrossClipboard.Current.SetText($"{m.Delete}\n{m.Consulta}\n{m.ConsultaCampoText}");
                await DisplayAlert("Consulta Copiada", "La consulta fue copiada, puedes pegarla donde quieras","Arre");
            }
            catch (Exception ex)
            {
                var mens = JsonConvert.DeserializeObject<MessageConsultaCampoModel>(response);

                resConsulta.Text = "";
                resConsultaCampo.Text = "";
                resConsulta.Text = mens.Message;

            }

        }
	}
}
