using System.ComponentModel.DataAnnotations;

namespace AnalizaEvaluarilor.Domain.Entity.User;

public class Role
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}