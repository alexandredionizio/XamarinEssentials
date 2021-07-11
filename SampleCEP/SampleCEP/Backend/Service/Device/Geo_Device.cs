using SampleCEP.Model.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SampleCEP.Backend.Service.Device
{
    public class Geo_Device
    {
        public async Task<Geo_Model> ObterUltimoLocalConhecido()
        {
            Geo_Model geo_model = null;

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                geo_model = new Geo_Model(location);
                
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                geo_model = new Geo_Model (mensagem: "Funcionalidade não disponível no seu aparelho.");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                geo_model = new Geo_Model(mensagem: "Funcionalidade não ligada no seu aparelho.");
            }
            catch (PermissionException pEx)
            {
                geo_model = new Geo_Model(mensagem: "Funcionalidade não tem permissão no seu aparelho.");
            }
            catch (Exception ex)
            {
                geo_model = ObterGeoModelException(ex);
            }


            return geo_model;
        }

        public async Task<Geo_Model> ObterPossivelLocal(string endereco)
        {
            Geo_Model geo_model = null;

            try
            {
                var locations = await Geocoding.GetLocationsAsync(endereco);
                var location = locations?.FirstOrDefault();

                geo_model = new Geo_Model(location);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                geo_model = ObterGeoFeatureNotSupportedException();
            }
            catch (Exception ex)
            {
                geo_model = ObterGeoModelException(ex);
            }

            return geo_model;
        }

        public async Task<Geo_Model> ObterPossivelEndereco(Location location)
        {
            Geo_Model geo_model = null;

            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location);

                var placemark = placemarks?.FirstOrDefault();

                return new Geo_Model(placemark: placemark);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                geo_model = ObterGeoFeatureNotSupportedException();
            }
            catch (Exception ex)
            {
                geo_model = ObterGeoModelException(ex);
            }

            return geo_model;


        }

        public double ObterDistanciaEntre(Geo_Model comeco_geo_model, Geo_Model fim_geo_model)
        {
            var distancia = comeco_geo_model.location.CalculateDistance(fim_geo_model.location, DistanceUnits.Kilometers);
            return distancia;
        }


        private static Geo_Model ObterGeoFeatureNotSupportedException()
        {
            return new Geo_Model(mensagem: "Funcionalidade não disponível no seu aparelho.");
        }

        private Geo_Model ObterGeoModelException(Exception ex)
        {
            return new Geo_Model(mensagem: $"Um cenário não previsto foi detectado, segue relatório tecnico: \n{ex.Message}");
        }
    }

}
