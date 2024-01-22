namespace BusinessObjects.Entities;

public class Role
{
    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
}