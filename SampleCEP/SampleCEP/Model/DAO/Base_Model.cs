using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleCEP.Model.DAO
{
    public class Base_Model
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
