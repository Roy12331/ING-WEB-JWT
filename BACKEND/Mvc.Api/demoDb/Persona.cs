using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Api.demoDb;

[Table("persona")]
[Index("IdPersonaTipo", Name = "idx_persona_tipo")]
[Index("IdTipoDocumento", Name = "idx_persona_tipo_documento")]
[Index("IdTipoSangre", Name = "idx_persona_tipo_sangre")]
[Index("IdTipoSexo", Name = "idx_persona_tipo_sexo")]
public partial class Persona
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_tipo_documento")]
    public int IdTipoDocumento { get; set; }

    [Column("id_persona_tipo")]
    public int IdPersonaTipo { get; set; }

    [Column("id_tipo_sexo")]
    public int IdTipoSexo { get; set; }

    [Column("id_tipo_sangre")]
    public int IdTipoSangre { get; set; }

    [Column("nombres")]
    [StringLength(100)]
    public string? Nombres { get; set; }

    [Column("apellido_paterno")]
    [StringLength(100)]
    public string? ApellidoPaterno { get; set; }

    [Column("apellido_materno")]
    [StringLength(100)]
    public string? ApellidoMaterno { get; set; }

    [Column("direccion")]
    [StringLength(300)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [StringLength(30)]
    public string? Telefono { get; set; }

    [Column("user_create")]
    public int UserCreate { get; set; }

    [Column("user_update")]
    public int? UserUpdate { get; set; }

    [Column("date_created", TypeName = "timestamp")]
    public DateTime? DateCreated { get; set; }

    [Column("date_update", TypeName = "timestamp")]
    public DateTime? DateUpdate { get; set; }

    [ForeignKey("IdPersonaTipo")]
    [InverseProperty("Persona")]
    public virtual PersonaTipo IdPersonaTipoNavigation { get; set; } = null!;

    [ForeignKey("IdTipoDocumento")]
    [InverseProperty("Persona")]
    public virtual PersonaTipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;

    [ForeignKey("IdTipoSangre")]
    [InverseProperty("Persona")]
    public virtual PersonaTipoSangre IdTipoSangreNavigation { get; set; } = null!;

    [ForeignKey("IdTipoSexo")]
    [InverseProperty("Persona")]
    public virtual PersonaTipoSexo IdTipoSexoNavigation { get; set; } = null!;
}
