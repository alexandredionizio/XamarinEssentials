using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace SampleCEP.Model.DAO
{
    [Table("Cep")]
    public class Cep_Model : Base_Model
    {
        [SQLite.Ignore]
        public int IdCep
        {
            get { return Id; }
            set { Id = value; }
        }

        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string unidade { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }


        public override string ToString()
        {            
            return $"{cep} : {logradouro}";
            // return cep + " : " + logradouro;
        }
    }    

}

/*
{
  "cep": "15015-100",
  "logradouro": "Rua Antônio de Godoy",
  "complemento": "de 2300/2301 a 4598/4599",
  "bairro": "Centro",
  "localidade": "São José do Rio Preto",
  "uf": "SP",
  "unidade": "",
  "ibge": "3549805",
  "gia": "6476"
}
 */
