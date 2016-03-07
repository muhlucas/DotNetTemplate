using DotNetTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTemplate.Infrastructure.Database.Context.Configuration
{
    internal class BaseEntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public BaseEntityConfiguration(string tableName)
        {
            ToTable(tableName);

            HasKey(x => x.Id)
                .HasEntitySetName("id");

            Property(x => x.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");

        }
    }
}
