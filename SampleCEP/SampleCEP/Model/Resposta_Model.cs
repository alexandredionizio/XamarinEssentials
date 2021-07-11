using System;
using System.Collections.Generic;
using System.Text;
using SampleCEP.Model.DAO;

namespace SampleCEP.Model
{
    public class Resposta_Model
    {
        public bool Sucesso { get; set; }
        public Cep_Model cep_model { get; set; }
        public Exception exception { get; set; }
    }
}
