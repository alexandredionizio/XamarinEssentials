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
    public partial class MenuPage : ContentPage
    {
        Version_Device version_device;

        public MenuPage()
        {
            InitializeComponent();
            version_device = new Version_Device();

            this.textVersion.Text = version_device.ObterVersaoAtual();
        }

        private async void buttonCrudCep_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void buttonConnectivity_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConectividadePage());
        }

        private async void buttonVibration_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VibrationPage());
        }

        private async void buttonCamera_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraPage());
        }

        private async void buttonGeo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GeoPage());
        }
    }
}