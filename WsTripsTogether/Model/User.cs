using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsTripsTogether.Model;

[Table("user")]
public class User
{
    [Column("id"), Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init;  }
    
    [Column("first_name"), Required, StringLength(100)]
    public required string FirstName { get; set; }
    
    [Column("last_name"), Required, StringLength(100)]
    public required string LastName { get; set; }
    
    [Column("username"), Required, StringLength(25)]
    public required string Username { get; set; }
    
    [Column("password"), Required, StringLength(250)]
    public required string Password { get; set; }
    
    [Column("email"), Required, StringLength(100)]
    public required string Email { get; set; }
    
    [Column("is_admin"), Required, DefaultValue(false)]
    public bool IsAdmin { get; init; }
}