using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DotNetTemplate.Infrastructure.Database.Context
{
    public class TemplateContext : DbContext, IDisposable
    {
        public TemplateContext(): base("name=Connection")
        { }
        
        //public DbSet<Example> Example { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            //modelBuilder.Configurations.Add(new ExampleConfiguration());

        }
    }
}
