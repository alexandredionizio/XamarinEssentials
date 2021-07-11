using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace SampleCEP.Backend.Service.Device
{
    public class Vibration_Device
    {

        public bool Vibrar()
        {
            try
            {
                Vibration.Vibrate(10000);
                return true;
            }
            catch (FeatureNotSupportedException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PararDeVibrar()
        {
            try
            {
                Vibration.Cancel();
                return true;
            }
            catch (FeatureNotSupportedException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
