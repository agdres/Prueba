using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackEnd.Models.DB
{
    public partial class Double_VContext : DbContext
    {
        public Double_VContext()
        {
        }

        public Double_VContext(DbContextOptions<Double_VContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Identificador)
                    .HasName("PK__Personas__F2374EB1A88C5A88");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.IdentificacionCompleta)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("Identificacion_Completa");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombresCompletos)
                    .HasMaxLength(210)
                    .IsUnicode(false)
                    .HasColumnName("Nombres_Completos");

                entity.Property(e => e.NumeroIdentificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Numero_Identificacion");

                entity.Property(e => e.TipoIdentificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Identificacion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Identificador)
                    .HasName("PK__Usuarios__F2374EB116827731");

                entity.HasIndex(e => e.Usuario1, "UQ__Usuarios__E3237CF738F1DE75")
                    .IsUnique();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Creacion");

                entity.Property(e => e.IdPersona).HasColumnName("ID_Persona");

                entity.Property(e => e.Pass)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Usuario");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK__Usuarios__Fecha___398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
