using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleCEP.Backend.Service.Device
{
    public class Message_Device
    {
        Page page;

        public Message_Device(Page page)
        {
            this.page = page;
        }

        public async Task Aviso(string mensagem)
        {
            await this.page.DisplayAlert("Aviso", mensagem, "OK");
        }

        public async Task<bool> Pergunta(string conteudo)
        {
            var resposta = await this.page.DisplayAlert("Pergunta", conteudo, "Sim","Não");

            return resposta;
        }


    }
}
