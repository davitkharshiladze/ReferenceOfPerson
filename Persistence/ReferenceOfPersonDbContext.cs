using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Persistence
{
    public class ReferenceOfPersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Relationship>()
                .HasKey(r => new { r.PersonId, r.RelatedPersonId, r.Type });

            modelBuilder.Entity<Relationship>()
                .HasRequired(r => r.Person)
                .WithMany(p => p.Relationships)
                .HasForeignKey(r => r.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Relationship>()
                .HasRequired(r => r.RelatedPerson)
                .WithMany()
                .HasForeignKey(r => r.RelatedPersonId)
                .WillCascadeOnDelete(false);

        }

        public ReferenceOfPersonDbContext() : base("ReferenceOfPerson")
        {
            Database.SetInitializer<ReferenceOfPersonDbContext>(new CreateDatabaseIfNotExists<ReferenceOfPersonDbContext>());
        }

        
    }
}