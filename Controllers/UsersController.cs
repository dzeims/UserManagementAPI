using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = new()
    {
        new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
        new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
    };

    // GET: api/users
    [HttpGet]
    public IActionResult GetAllUsers() => Ok(Users);

    // GET: api/users/5
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        return user == null ? NotFound() : Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
        user.Id = Users.Max(u => u.Id) + 1;
        Users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.Email = updatedUser.Email;
        return NoContent();
    }

    // DELETE: api/users/5
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        Users.Remove(user);
        return NoContent();
    }
}