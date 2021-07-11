using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.Linq;

namespace SampleCEP.Backend.Service.Device
{
    public class Connnectivity_Device
    {
        public bool TemBandaLarga { get; private set; }

        public event EventHandler<ConnectivityEventArgs> ConnectionChanged;

        public Connnectivity_Device()
        {
            InitializeComponent(Connectivity.NetworkAccess, Connectivity.ConnectionProfiles);
        }

        private void InitializeComponent(NetworkAccess current, IEnumerable<ConnectionProfile> conexoes)
        {
            var listBandaLarga = conexoes.Where(c => c == ConnectionProfile.WiFi || c == ConnectionProfile.Ethernet);
            TemBandaLarga = (current == NetworkAccess.Internet && ( listBandaLarga != null || listBandaLarga.Count() > 0));
        }

        public void OnConnectionChanged(object sender, ConnectivityChangedEventArgs args) 
        {
            InitializeComponent(args.NetworkAccess, args.ConnectionProfiles);

            if (ConnectionChanged != null)
                ConnectionChanged(sender, new ConnectivityEventArgs { TemConexaoBandaLarga = this.TemBandaLarga });
        }


        public bool VerificaConexaoBandaLarga()
        {
            return TemBandaLarga;
        }
    }

    public class ConnectivityEventArgs : EventArgs
    {
        public bool TemConexaoBandaLarga { get; set; }
    }
}
