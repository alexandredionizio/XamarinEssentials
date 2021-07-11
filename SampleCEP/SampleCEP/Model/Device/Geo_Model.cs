using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace SampleCEP.Model.Device
{
    public class Geo_Model
    {       
        public bool sucesso;
        public string mensagem;



        public Location location;
        public Placemark placemark;

        internal double latitude;
        internal double longitude;

        public string adminArea;
        public string countryCode;
        public string countryName;
        public string featureName;
        public string locality;
        public string postalCode;
        public string subAdminArea;
        public string subLocality;
        public string subThoroughfare;
        public string thoroughfare;



        public Geo_Model(Location location = null,Placemark placemark = null,bool sucesso = false,string mensagem = "")
        {            
            this.sucesso = sucesso;
            this.mensagem = mensagem;

            this.location = location;
            this.placemark = placemark;

            if (placemark == null)
            {
                this.sucesso = false;
                this.mensagem = "Não foi possível identificar o seu endereço.";
            }
            else
            {
                this.location = placemark.Location;

                this.adminArea = placemark.AdminArea;
                this.countryCode = placemark.CountryCode;
                this.countryName = placemark.CountryName;
                this.featureName = placemark.FeatureName;
                this.locality = placemark.Locality;
                this.postalCode = placemark.PostalCode;
                this.subAdminArea = placemark.SubAdminArea;
                this.subLocality = placemark.SubLocality;
                this.subThoroughfare = placemark.SubThoroughfare;
                this.thoroughfare = placemark.Thoroughfare;
            }


            if (this.location == null)
            {
                this.sucesso = false;
                this.mensagem = "Não foi possível identificar o seu local atual.";
            }
            else
            {
                this.sucesso = true;
                this.latitude = this.location.Latitude;
                this.longitude = this.location.Longitude;
            }
        }
    }
}
