using System;
using System.Collections.Generic;

namespace WPFECZV1.Models;

public partial class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public int RoleId { get; set; }

    public virtual Role Role { get; set; }
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
