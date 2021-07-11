
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SampleCEP.Backend.Service.Device;

namespace SampleCEP.Frontend.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        Camera_Device camera_device;

        public CameraPage()
        {
            InitializeComponent();

            camera_device = new Camera_Device();
        }
        private async Task SetFoto(Foto_Model foto_model)
        {
            if (foto_model == null)
            {
                await this.DisplayAlert("Aviso", "Não foi possível tirar foto", "OK");
                return;
            }

            this.imageGaleria.Source = foto_model.GaleriaPath;
            this.imageInterno.Source = foto_model.InternoPath;
            this.imageArray.Source = camera_device.ToImageSource(foto_model.arrayValue);
        }



        private async void buttonCamera_Clicked(object sender, EventArgs e)
        {
            var foto_model = await camera_device.TirarFoto();

            await SetFoto(foto_model);
        }
        private async void buttonEscolherFoto_Clicked(object sender, EventArgs e)
        {
            var foto_model = await camera_device.EscolherFotoGaleria();
            await SetFoto(foto_model);
        }
    }   
}