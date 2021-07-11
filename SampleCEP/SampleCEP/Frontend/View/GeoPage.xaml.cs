using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SampleCEP.Backend.Service.Device;
using SampleCEP.Model.Device;

namespace SampleCEP.Frontend.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoPage : ContentPage
    {

        Geo_Device geo_device;
        Message_Device message_device;

        public GeoPage()
        {
            InitializeComponent();

            this.geo_device = new Geo_Device();
            this.message_device = new Message_Device(this);
        }

        private async void buttonUltimoLocalConhecido_Clicked(object sender, EventArgs e)
        {
            var geo_model = await this.geo_device.ObterUltimoLocalConhecido();
            await AvisaLatitudeLongitude(geo_model);

            geo_model = await this.geo_device.ObterPossivelEndereco(geo_model.location);
            await AvisaEndereco(geo_model);

            geo_model = await this.geo_device.ObterPossivelLocal(geo_model.thoroughfare);
            await AvisaLatitudeLongitude(geo_model);
        }

        private async Task AvisaEndereco(Geo_Model geo_model)
        {
            await this.message_device.Aviso($"Seu endereço\n" +
                                            $"AdminArea:       {geo_model.adminArea}\n" +
                                            $"CountryCode:     {geo_model.countryCode}\n" +
                                            $"CountryName:     {geo_model.countryName}\n" +
                                            $"FeatureName:     {geo_model.featureName}\n" +
                                            $"Locality:        {geo_model.locality}\n" +
                                            $"PostalCode:      {geo_model.postalCode}\n" +
                                            $"SubAdminArea:    {geo_model.subAdminArea}\n" +
                                            $"SubLocality:     {geo_model.subLocality}\n" +
                                            $"SubThoroughfare: {geo_model.subThoroughfare}\n" +
                                            $"Thoroughfare:    {geo_model.thoroughfare}\n");
        }

        private async Task AvisaLatitudeLongitude(Geo_Model geo_model)
        {
            if (geo_model == null)
            {
                await this.message_device.Aviso("Não foi possível obter seu local.");
                return;
            }
            else if (!geo_model.sucesso)
            {
                await this.message_device.Aviso(geo_model.mensagem);
                return;
            }
            else if (geo_model.sucesso)
                await this.message_device.Aviso($"Você esta na latitude:{geo_model.latitude}, longitude:{geo_model.longitude}");
        }

        private async void buttonDistancia_Clicked(object sender, EventArgs e)
        {
            var comeco_geo_model = await this.geo_device.ObterUltimoLocalConhecido();
            var comeco_endereco = await this.geo_device.ObterPossivelEndereco(comeco_geo_model.location);

            var fim_geo_model = await this.geo_device.ObterPossivelLocal(entryRuaFim.Text);
            var fim_endereco = await this.geo_device.ObterPossivelEndereco(fim_geo_model.location);

            var distancia = this.geo_device.ObterDistanciaEntre(comeco_geo_model, fim_geo_model);

            await message_device.Aviso($"Sua Distancia entre\n{comeco_endereco.thoroughfare}\naté\n{fim_endereco.thoroughfare}\né de {distancia} kms");

        }
    }
}