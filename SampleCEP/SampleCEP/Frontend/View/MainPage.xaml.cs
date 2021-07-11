// sistema
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

//Service
using SampleCEP.Backend.Service;
using SampleCEP.Backend.Service.Device;

namespace SampleCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private Pessoa_Service pessoa_service = new Pessoa_Service();
        private Cep_Service cep_Service = new Cep_Service();
        private Vibration_Device vibration_device;

        public MainPage()
        {
            InitializeComponent();
            vibration_device = new Vibration_Device();
        }

        private async void btnBuscarEndereco_Clicked(object sender, EventArgs e)
        {
           await Task.Run(VibrarAsync);

            var cep = etCep.Text;

            var resposta = await cep_Service.Get(cep);

            vibration_device.PararDeVibrar();
            if (resposta.Sucesso)
            {
                this.cep_Service.Create(resposta.cep_model);

                await DisplayAlert(cep,
                    "Logradouro " + resposta.cep_model.logradouro +
                    "\nBairro " + resposta.cep_model.bairro +
                    "\nLogradouro " + resposta.cep_model.uf,
                    "OK");
            }
            else
                await DisplayAlert("Aviso",
                    "Não foi possivel efetuar a operação\n" + resposta.exception.Message,
                    "OK");
        }

        private async Task VibrarAsync()
        {
            vibration_device.Vibrar();
        }

        private void btnBuscarCepBanco_Clicked(object sender, EventArgs e)
        {
            var cep_model = this.cep_Service.Read(etFiltroIdCep.Text);
            
            bool encontrouCep = (cep_model != null);
            var msg = encontrouCep ? cep_model.ToString() : "Nenhum Cep Encontrado";

            if(encontrouCep)
                this.etComplemento.Text = cep_model.complemento;

            DisplayAlert("Aviso",
                msg ,
                "OK");

        }

        private void btnExcluiCep_Clicked(object sender, EventArgs e)
        {
            try
            {
                this.cep_Service.Delete(etId.Text);
                DisplayAlert("Aviso","Cep excluido com sucesso.", "OK");
            }
            catch (Exception exception)
            {
                DisplayAlert("Aviso", exception.Message, "OK");                
            }
            
        }

        private void btnEditaCep_Clicked(object sender, EventArgs args)
        {
            try
            {
                this.cep_Service.Update(etId.Text, etComplemento.Text);
                DisplayAlert("Aviso", "Cep atualizado com sucesso.", "OK");
            }
            catch (Exception e)
            {
                DisplayAlert("Aviso", e.Message, "OK");
            }
        }
    }
}
