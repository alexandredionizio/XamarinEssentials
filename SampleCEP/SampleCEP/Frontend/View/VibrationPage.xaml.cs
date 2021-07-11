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
    public partial class VibrationPage : ContentPage
    {
        Vibration_Device vibration_device;
        Message_Device message_device;

        public VibrationPage()
        {
            InitializeComponent();
            vibration_device = new Vibration_Device();
            message_device = new Message_Device(this);
        }

        private async void buttonStartVibration_Clicked(object sender, EventArgs e)
        {
           var vibrou = vibration_device.Vibrar();

            if (!vibrou)
                await this.message_device.Aviso("Esse dipositivo não pode vibrar");
        }

        private void buttonFinishVibration_Clicked(object sender, EventArgs e)
        {
            vibration_device.PararDeVibrar();
        }
    }
}