using Delivery.Domain.Entities;
using Delivery.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Deliveryx> Deliveries { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<DeliveryRoute> DeliveryRoute { get; set; }
        public DbSet<Address> Addresses { get; set; }  // <-- Asegúrate de que esto está aquí

       // public DbSet<DeliveryPerson> DeliveryPersons { get; set; } // 👈 Debe coincidir con la tabla

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Address");// 👈 Forzar el nombre correcto
            modelBuilder.Entity<DeliveryRoute>().ToTable("DeliveryRoute");// 👈 Forzar el nombre correcto
            modelBuilder.Entity<DeliveryPerson>().ToTable("DeliveryPerson");// 👈 Forzar el nombre correcto
            modelBuilder.Entity<Deliveryx>().ToTable("Deliveryx");// 👈 Forzar el nombre correcto


            // Definir la tabla y la clave primaria
            modelBuilder.Entity<Deliveryx>()
                .ToTable("Deliveryx")
                .HasKey(d => d.Id);


            modelBuilder.Entity<Deliveryx>()
                .Property(d => d.ScheduledDate)
                .HasColumnType("datetime2");

            // Configurar las propiedades con nombres coincidentes en la base de datos
            modelBuilder.Entity<Deliveryx>()
                .Property(d => d.DeliveryAddressId)
                .HasColumnName("DeliveryAddressId");

            modelBuilder.Entity<Deliveryx>()
                .Property(d => d.RouteId)  // Cambiado de DeliveryRouteId a RouteId
                .HasColumnName("RouteId");

            modelBuilder.Entity<Deliveryx>()
                .Property(d => d.FechaEntrega)
                .HasColumnName("FechaEntrega")
                .HasColumnType("datetime2");


            modelBuilder.Entity<Deliveryx>()
                .Property(d => d.DeliveryPersonId)
                .HasColumnName("DeliveryPersonId");

            modelBuilder.Entity<Deliveryx>()
                .Property(d => d.Status)
                .HasColumnName("Status");

            // Relación con Address
            modelBuilder.Entity<Deliveryx>()
                .HasOne<Address>()  // No se define navegación explícita
                .WithMany()
                .HasForeignKey(d => d.DeliveryAddressId);

            // Relación con DeliveryRoute
            modelBuilder.Entity<Deliveryx>()
                .HasOne(d => d.Route)
                .WithMany(r => r.Deliveries)
                .HasForeignKey(d => d.RouteId);

            //// Relación con DeliveryPerson
            //modelBuilder.Entity<Deliveryx>()
            //    .HasOne(d => d.AssignedPerson)
            //    .WithMany()
            //    .HasForeignKey(d => d.DeliveryPersonId);

            // Relación con DeliveryPerson
            modelBuilder.Entity<Deliveryx>()
                .HasOne(d => d.AssignedPerson) // ✅ Asegurar que existe la propiedad AssignedPerson en Deliveryx
                .WithMany(p => p.Deliveries) // ✅ Relación inversa
                .HasForeignKey(d => d.DeliveryPersonId)
                .OnDelete(DeleteBehavior.SetNull); // Si se borra el DeliveryPerson, no eliminar el Deliveryx



            // Relación con Package (uno a muchos)
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Delivery)
                .WithMany(d => d.Packages)
                .HasForeignKey(p => p.DeliveryId);

            base.OnModelCreating(modelBuilder);

        }




         
    }
        
}





/*
modelBuilder.Entity<Package>().HasKey(p => p.Id);
//modelBuilder.Entity<Deliveryx>().OwnsOne(d => d.DeliveryAddress); // Esto marca DeliveryAddress como tipo complejo
// Forzar que la entidad `Address` use la tabla `Address` en lugar de `Addresses`
modelBuilder.Entity<Address>().ToTable("Address");

modelBuilder.Entity<Address>()
 .HasKey(a => a.Id);  // ✅ Definir la clave primaria


modelBuilder.Entity<DeliveryRoute>()
    .HasKey(r => r.Id);  // ✅ Definir la clave primaria correctamente

// Configurar la relación entre Deliveryx y DeliveryRoute
modelBuilder.Entity<Deliveryx>()
    .HasOne(d => d.Route)  // Deliveryx tiene una relación con DeliveryRoute
    .WithMany(r => r.Deliveries)  // DeliveryRoute puede tener múltiples Deliveryx
    .HasForeignKey(d => d.RouteId);  // La clave foránea está en Deliveryx


modelBuilder.Entity<Deliveryx>()
             .Property(d => d.FechaEntrega)
             .HasColumnName("FechaEntrega");

// Relación entre Package y Deliveryx (uno a muchos)
modelBuilder.Entity<Package>()
    .HasOne(p => p.Delivery)
    .WithMany(d => d.Packages)
    .HasForeignKey(p => p.DeliveryId);
*/
