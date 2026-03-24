using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel.demoDb;

[Table("persona_tipo_sexo")]
public partial class PersonaTipoSexo
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

    [InverseProperty("IdTipoSexoNavigation")]
    public virtual ICollection<Persona> Persona { get; set; } = new List<Persona>();
}
