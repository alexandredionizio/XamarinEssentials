using SampleCEP.Backend.Service.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleCEP.Frontend.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConectividadePage : ContentPage
    {
        Connnectivity_Device connnectivity_device;

        public ConectividadePage()
        {
            InitializeComponent();
            // comportamento padrao
            connnectivity_device = new Connnectivity_Device();

            //evento para disparar automaticamente quando a conexao for modificada
            connnectivity_device.ConnectionChanged += QuandoConexaoModificada;
        }

        private void QuandoConexaoModificada(object sender, ConnectivityEventArgs e)
        {
            var temConexaoBandaLarga = e.TemConexaoBandaLarga;
            AvisaSobreConexao(temConexaoBandaLarga);
        }

        // comportamento padrao
        private void buttonTesteConexao_Clicked(object sender, EventArgs e)
        {
            var temConexaoBandaLarga = connnectivity_device.VerificaConexaoBandaLarga();
            AvisaSobreConexao(temConexaoBandaLarga);
        }

        //evento para disparar automaticamente quando a conexao for modificada
        private void AvisaSobreConexao(bool temConexaoBandaLarga)
        {
            if (temConexaoBandaLarga)
                DisplayAlert("Aviso", "Tem conexão banda larga.", "ok");
            else
                DisplayAlert("Aviso", "Não tem conexão banda larga.", "ok");
        }
    }
}