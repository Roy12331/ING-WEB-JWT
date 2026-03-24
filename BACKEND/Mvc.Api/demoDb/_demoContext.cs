using DbModel.demoDb;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using System;
using System.Collections.Generic;

namespace Mvc.Api.demoDb;

public partial class _demoContext : DbContext
{
    public _demoContext()
    {
    }

    public _demoContext(DbContextOptions<_demoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Persona { get; set; }

    public virtual DbSet<PersonaTipo> PersonaTipo { get; set; }

    public virtual DbSet<PersonaTipoDocumento> PersonaTipoDocumento { get; set; }

    public virtual DbSet<PersonaTipoSangre> PersonaTipoSangre { get; set; }

    public virtual DbSet<PersonaTipoSexo> PersonaTipoSexo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=demoDb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdPersonaTipoNavigation).WithMany(p => p.Persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_tipo");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_tipo_documento");

            entity.HasOne(d => d.IdTipoSangreNavigation).WithMany(p => p.Persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_tipo_sangre");

            entity.HasOne(d => d.IdTipoSexoNavigation).WithMany(p => p.Persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_tipo_sexo");
        });

        modelBuilder.Entity<PersonaTipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<PersonaTipoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<PersonaTipoSangre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<PersonaTipoSexo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
