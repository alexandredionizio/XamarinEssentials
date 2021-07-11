
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace SampleCEP.Backend.Service.Device
{
    public class Camera_Device
    {
        public async Task<Foto_Model> TirarFoto(string nomeFoto = "padrao.jpg", string diretorio= "Fotos", bool saveInAlbum = true)
        {
            Foto_Model foto_model = null;
            var currentMedia = CrossMedia.Current;
                
            if (!currentMedia.IsTakePhotoSupported || !currentMedia.IsCameraAvailable)
                return null;
            
            var photoCross = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Name = nomeFoto,
                Directory = diretorio,
                SaveToAlbum = saveInAlbum,
                CompressionQuality = 10,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CustomPhotoSize = 10,
            });
            
            if (photoCross == null) return null;

            foto_model = SetFoto_Model(photoCross);

            return foto_model;
        }
        public async Task<Foto_Model> EscolherFotoGaleria()
        {
            Foto_Model foto_model = null;
            var currentMedia = CrossMedia.Current;

            if (!currentMedia.IsPickPhotoSupported)
                return null;

            var photoCross = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                CompressionQuality = 50
            });

            if (photoCross == null)
                return null;

            foto_model = SetFoto_Model(photoCross);

            return foto_model;
        }

        private Foto_Model SetFoto_Model(MediaFile photoCross)
        {
            return new Foto_Model
            {
                GaleriaPath = photoCross.AlbumPath,
                InternoPath = photoCross.Path,
                arrayValue = this.ToByteArray(photoCross.GetStream())
            };
        }

        public byte[] ToByteArray(Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];

            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);

            return buffer;
        }

        public ImageSource ToImageSource(Stream stream)
        {
            return ImageSource.FromStream(() => stream);
        }

        public ImageSource ToImageSource(byte[] bytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }


    }


    public class Foto_Model
    {
        public string GaleriaPath;
        public string InternoPath;
        public byte[] arrayValue;
    }
}
