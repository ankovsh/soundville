using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Soundville.Domain.EntityFramework;
using Soundville.Domain.Models.Interfaces;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Services
{
    public class DomainService<TModel> : IDomainService<TModel> where TModel : class, IBaseDomainModel
    {
        protected readonly ISoundvilleContext Context;

        protected DomainService(ISoundvilleContext soundvilleContext)
        {
            Context = soundvilleContext;
        }

        protected DbSet<TModel> DbSet
        {
            get { return Context.Set<TModel>(); }
        }

        public TModel GetById(int id)
        {
            return Context.Set<TModel>().Find(id);
        }

        public IList<TModel> GetAll()
        {
            return Context.Set<TModel>().ToList();
        }

        public TModel Create(TModel model)
        {
            Context.Set<TModel>().Add(model);
            Context.SaveChanges();
            return model;
        }

        public TModel Update(TModel updated)
        {
            if (updated == null)
            {
                return null;
            }

            TModel existing = GetById(updated.Id);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }

            return existing;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public void Delete(IList<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = GetById(id);
                DbSet.Remove(entity);
            }

            Context.SaveChanges();
        }

        public void Delete(TModel model)
        {
            Context.Set<TModel>().Remove(model);
            Context.SaveChanges();
        }
    }
}
