using SampleCEP.Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SampleCEP.Backend.Repository
{
    public class Base_Repository<T> 
        where  T : Base_Model, new()
    {
        public void Create(T model)
        {
            using (var conn = Conexao.Get())
            {
                conn.BeginTransaction();
                conn.Insert(model);
                conn.Commit();
            }
        }
        public IEnumerable<T> Read()
        {
            using (var conn = Conexao.Get())
            {
                conn.BeginTransaction();
                var lista = conn.Table<T>().ToList();
                conn.Commit();

                return lista;
            }            
        }

        public void Update(T model)
        {
            using (var conn = Conexao.Get())
            {
                conn.BeginTransaction();
                conn.Update(model);
                conn.Commit();
            }
        }
        public void Delete(T model)
        {            
            using (var conn = Conexao.Get())
            {
                conn.BeginTransaction();
                conn.Delete<T>(model.Id);
                conn.Commit();
            }
        }
    }
}
