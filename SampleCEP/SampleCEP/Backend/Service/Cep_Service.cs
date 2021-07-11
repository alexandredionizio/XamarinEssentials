using System;
using System.Collections.Generic;
using System.Text;
//model
using SampleCEP.Model.DAO;
using SampleCEP.Model;
// Cliente de conexao rest
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SampleCEP.Backend.Repository;
using System.Linq;
using SampleCEP.Backend.Service.Device;

namespace SampleCEP.Backend.Service
{
    public class Cep_Service : Base_Service<Cep_Model>
    {
        private Cep_Repository repository;
        public string base_url;
        public string api_name;
        public HttpClient client;
        public Connnectivity_Device connnectivity_device;

        public Cep_Service()
        {
            repository = new Cep_Repository();

            base_url = "https://viacep.com.br";
            api_name = "/ws/{0}/json";
            client = GetClient();

            connnectivity_device = new Connnectivity_Device();
        }
        #region ConsumoApi
        protected HttpClient GetClient()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri(this.base_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            client.Timeout = new TimeSpan(0, 2, 0);

            return client;
        }
        public async Task<Resposta_Model> Get(string cep)
        {
            Resposta_Model resposta_model = new Resposta_Model();

            var temBandaLarga = connnectivity_device.VerificaConexaoBandaLarga();
            if (!temBandaLarga)
            {
                resposta_model.Sucesso = false;
                resposta_model.exception = new Exception("Não tem Internet para fazer sua conexao.");
                return resposta_model;
            }

            HttpResponseMessage response = null;
            try
            {
                string api_concatenada = string.Format(this.api_name, cep);
                response = await client.GetAsync(api_concatenada);

                resposta_model.Sucesso = response.IsSuccessStatusCode;

                if (resposta_model.Sucesso)
                {
                    var retornoTexto = await response.Content.ReadAsStringAsync();
                    resposta_model.cep_model = JsonConvert.DeserializeObject<Cep_Model>(retornoTexto);
                }
                else
                    resposta_model.exception = new Exception(response.ReasonPhrase);

            }
            catch (Exception exception)
            {
                resposta_model.Sucesso = false;
                resposta_model.exception = exception;
            }

            return resposta_model;
        }

        public override Cep_Model Read(string filtro)
        {
            filtro = filtro.ToLower();
            int id = 0;
            bool porId = int.TryParse(filtro, out id);

            Cep_Model primeiroCepEncontrado = this.repository.Read()
                .Where(cep =>
                        (porId ? cep.Id == id : false) ||
                        cep.logradouro.ToLower().Contains(filtro) ||
                        cep.uf.ToLower().Contains(filtro)
                        )
                .FirstOrDefault();


            return primeiroCepEncontrado;

        }

        internal void Update(string textId, string textComplemento)
        {
            int id = this.ConverteId(textId);
            var cep_model = this.Read(id);
            cep_model.complemento = textComplemento;

            base.Update(textId, cep_model);
        }
        #endregion


    }


}
