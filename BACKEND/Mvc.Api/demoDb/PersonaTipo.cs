using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Api.demoDb;

[Table("persona_tipo")]
public partial class PersonaTipo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("codigo")]
    [StringLength(20)]
    public string? Codigo { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdPersonaTipoNavigation")]
    public virtual ICollection<Persona> Persona { get; set; } = new List<Persona>();
}
