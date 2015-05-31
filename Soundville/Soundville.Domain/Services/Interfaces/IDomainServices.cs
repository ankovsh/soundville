using System.Collections.Generic;
using Soundville.Domain.Models.Interfaces;

namespace Soundville.Domain.Services.Interfaces
{
    public interface IDomainService<TModel> where TModel : class, IBaseDomainModel
    {
        TModel GetById(int id);
        IList<TModel> GetAll();
        TModel Create(TModel model);
        TModel Update(TModel updated);
        void Delete(int id);
        void Delete(IList<int> ids);
        void Delete(TModel model);
        void SaveChanges();
    }
}
