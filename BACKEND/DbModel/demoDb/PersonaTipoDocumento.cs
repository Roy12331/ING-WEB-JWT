using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("persona_tipo_documento")]
public partial class PersonaTipoDocumento
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("codigo")]
    [StringLength(20)]
    public string Codigo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(50)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdTipoDocumentoNavigation")]
    public virtual ICollection<Persona> Persona { get; set; } = new List<Persona>();
}
