using System.ComponentModel.DataAnnotations.Schema;

namespace AnalizaEvaluarilor.Domain.Entity.User;

public class UserRole
{
    
    public Guid IdUser { get; set; }
    [ForeignKey("IdUser")]
    public Entity.User.User User { get; set; }
    public Guid IdRole { get; set; }
    [ForeignKey("IdRole")]
    public Role Role { get; set; }
    
}