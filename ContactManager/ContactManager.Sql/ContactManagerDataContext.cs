using ContactManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactManager.Sql
{
    public class ContactManagerDataContext : DbContext
    {
        public ContactManagerDataContext(DbContextOptions<ContactManagerDataContext> options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<PhoneNumber>().ToTable("PhoneNumber");

            modelBuilder.Entity<Contact>(entity =>
            {
                entity
                    .HasMany(e => e.Relations)
                    .WithOne(e => e.Parent)
                    .HasForeignKey(e => e.ParentId)
                    .OnDelete(DeleteBehavior.Cascade);

                //entity.HasMany(e => e.PhoneNumbers)
                //    .WithOne(e => e.Contact)
                //    .OnDelete(DeleteBehavior.Cascade);

                //entity.HasMany(e => e.Addresses)
                //    .WithOne(e => e.Contact)
                //    .OnDelete(DeleteBehavior.Cascade); ;
            });

        }
    }
}
