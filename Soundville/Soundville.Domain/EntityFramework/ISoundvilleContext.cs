using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using Soundville.Domain.Models;

namespace Soundville.Domain.EntityFramework
{
    public interface ISoundvilleContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Station> Stations { get; set; }

        #region Standart DbContext
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbEntityEntry Entry(object entity);
        Type GetType();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        #endregion

    }
}
