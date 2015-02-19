﻿using System.Data.Entity;
using System.Reflection;
using Soundville.Domain.EntityFramework.Configurations;
using Soundville.Domain.Models;

namespace Soundville.Domain.EntityFramework
{
    class SoundvilleContext : DbContext, ISoundvilleContext
    {
        public DbSet<User> Users { get; set; }

        public SoundvilleContext()
            : base("SoundvilleContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
