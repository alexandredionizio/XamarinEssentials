
using System;
using System.Collections.Generic;
using System.Text;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using SampleCEP.Model;
using SampleCEP.Model.DAO;
using SQLite;

namespace SampleCEP.Backend.Repository
{

    public class Conexao
    {
        public static string Nome_DB = "dados.db";
        public static SQLiteConnection Get()
        {
            var folder = new LocalRootFolder();
            var arquivo = folder.CreateFile(Nome_DB, CreationCollisionOption.OpenIfExists);

            return new SQLiteConnection(arquivo.Path, false);
        }

        public void IniciarBanco()
        {
            using (SQLiteConnection conn = Get())
            {
                conn.BeginTransaction();
                conn.CreateTable<Cep_Model>();
                conn.CreateTable<Perfil_Model>();
                 
                conn.Commit();
            }
        }
    }

}
