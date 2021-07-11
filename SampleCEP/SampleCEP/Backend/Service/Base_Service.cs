using SampleCEP.Backend.Repository;
using SampleCEP.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCEP.Backend.Service
{
    public class Base_Service<T>
         where T : Base_Model, new()
    {
        protected Base_Repository<T> repository;

        public Base_Service()
        {
            repository = new Base_Repository<T>();
        }

        #region ConsumoRepository
        public virtual void Create(T model)
        {
            // validarCreateGenerico();
            this.repository.Create(model);
        }

        public virtual void Update(string textoId, T dadosParaAtualizar)
        {
            int id = ConverteId(textoId);
            dadosParaAtualizar.Id = id;
          
            repository.Update(dadosParaAtualizar);
        }

        public virtual T Read(string filtro)
        {
            int id = ConverteId(filtro);
            return Read(id);
        }
        public virtual T Read(int id)
        {
            var model = this.repository.Read()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            return model;
        }

        public virtual T ReadFirst()
        {
            return this.repository.Read().FirstOrDefault();
        }


        public virtual void Delete(string textoId)
        {
            int id = ConverteId(textoId);
            Delete(id);
        }

        public virtual void Delete(int id)
        {
            var model = new T();
            model.Id = id;

            this.repository.Delete(model);
        }

        #endregion

        protected int ConverteId(string textoId)
        {
            int id = 0;
            if (!int.TryParse(textoId, out id))
                throw new Exception("O texto do id não é um número.");

            return id;
        }

    }
}
