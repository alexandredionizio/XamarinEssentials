using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleCEP.Model.DAO
{
   [Table("Perfil")]
    public class Perfil_Model : Base_Model
    {
        public string Descricao { get; set; }


        public override string ToString()
        {
            return Id + " " + Descricao;
        }
    }
}
