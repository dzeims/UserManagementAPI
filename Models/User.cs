namespace UserManagementAPI.Models;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}