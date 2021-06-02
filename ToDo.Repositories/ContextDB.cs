using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = ToDo.Entities;

namespace ToDo.Repositories
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }
        public DbSet<Entities.ToDo> Todos { get; set; }
        public DbSet<Entities.Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.ToDo>(entity =>
            {
                entity.HasKey(e => e.Id);

                //entity.Property<int>("Id");

                entity.HasOne(e => e.Folder)
                     .WithMany(e => e.Todos);
                    // .HasForeignKey(e => e.FolderId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Entities.Folder>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Todos)
                    .WithOne(e => e.Folder);



                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

            });

            #region Add Initial data
            modelBuilder.Entity<Entities.Folder>().HasData(
                new Entities.Folder { Id = 1, Name = "Root", Enabled = true });
            #endregion

        }
    }
}
