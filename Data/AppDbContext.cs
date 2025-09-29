using Microsoft.EntityFrameworkCore;
using Taller_HU3.Models;

namespace Taller_HU3.Data;


public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Vet> Vets { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<MedicalAt>  MedicalAts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(
                "server=127.0.0.1;database=interstellar_HU_3;user=interstellar;password=int123!!",
                new MySqlServerVersion(new Version(8, 0, 36))
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MedicalAt>()
            .HasKey(ma => ma.Id);
        
        modelBuilder.Entity<MedicalAt>()
            .HasOne(ma => ma.Client)
            .WithMany(c => c.MedicalAts)
            .HasForeignKey(ma => ma.ClientId);
        
        modelBuilder.Entity<MedicalAt>()
            .HasOne(ma => ma.Pet)
            .WithMany(pet => pet.MedicalAts)
            .HasForeignKey(ma => ma.PetId);
        
        modelBuilder.Entity<MedicalAt>()
            .HasOne(ma  => ma.Vet)
            .WithMany(vet => vet.MedicalAts)
            .HasForeignKey(ma => ma.VetId);

        modelBuilder.Entity<Pet>()
            .HasOne(pet => pet.Client)
            .WithMany(c => c.Pets)
            .HasForeignKey(p => p.ClientId);
    }
}
