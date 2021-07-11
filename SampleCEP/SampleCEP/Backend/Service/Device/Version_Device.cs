using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace SampleCEP.Backend.Service.Device
{
    public class Version_Device
    {
        public string ObterVersaoAtual()
        {
            var versao = VersionTracking.CurrentVersion;

            return versao;
        }
    }
}
