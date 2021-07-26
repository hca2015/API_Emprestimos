using API_Emprestimos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace API_Emprestimos.Repository
{
    public abstract class AbstractRepository<T> : ClasseBase
        where T : AbstractModel
    {
        public AbstractRepository(BaseDbContext context, ContextoExecucao contexto) : base(contexto)
        {
            Context = context;
            Entity = context.Set<T>();
        }

        protected readonly DbSet<T> Entity;
        protected BaseDbContext Context { get; }

        private void Add(T abstractModel)
        {
            Entity.Add(abstractModel);
        }

        private T Find(T abstractModel)
        {
            return Entity.Find(abstractModel.getId());
        }

        private void Remove(T abstractModel)
        {
            Entity.Remove(abstractModel);
        }

        private bool Flush()
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            catch (Exception e)
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            {
                return false;
            }
        }

        public bool Insert(T abstractModel)
        {
            BeforeInsert(abstractModel);

            Add(abstractModel);

            Flush();

            return Flush();
        }

        protected virtual void BeforeInsert(T abstractModel)
        {

        }

        public bool Update(T abstractModel)
        {
            BeforeUpdate(abstractModel);

            var contextModel = Find(abstractModel);

            contextModel.Update(abstractModel);

            return Flush();
        }

        protected virtual void BeforeUpdate(T abstractModel)
        {

        }

        public bool Delete(int id)
        {
            var model = Entity.Find(id);

            if (model != null)
            {
                BeforeDelete(model);

                Remove(model);
            }

            return Flush();
        }

        protected virtual void BeforeDelete(T abstractModel)
        {

        }

        public void WipeData()
        {
            var all = Entity.ToList();
            foreach (var item in all)
            {
                Delete(item.getId());
            }
        }
    }
}
