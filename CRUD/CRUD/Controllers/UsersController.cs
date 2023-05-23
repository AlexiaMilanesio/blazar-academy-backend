using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    // RENDER

    [HttpGet("ShowAll")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            string file = await new StreamReader("bin/Debug/net7.0/CDriversDirs.txt").ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(file)) throw new Exception("There are no users yet");

            List<User>? users = JsonConvert.DeserializeObject<List<User>>(file);

            return Ok(users);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }


    [HttpGet("ShowDeleted")]
    public async Task<IActionResult> GetDeletedUsers()
    {
        try
        {
            string file = await new StreamReader("bin/Debug/net7.0/CDriversDirs.txt").ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(file)) throw new Exception("There are no users yet");

            List<User>? users = JsonConvert.DeserializeObject<List<User>>(file);
            List<User>? usersNotDeleted = users.FindAll(user => user.DeletedUser == true);

            return Ok(usersNotDeleted);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }


    [HttpGet("ShowNotDeleted")]
    public async Task<IActionResult> GetNotDeletedUsers()
    {
        try
        {
            string file = await new StreamReader("bin/Debug/net7.0/CDriversDirs.txt").ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(file)) throw new Exception("There are no users yet");

            List<User>? users = JsonConvert.DeserializeObject<List<User>>(file);
            List<User>? usersNotDeleted = users.FindAll(user => user.DeletedUser == false);

            return Ok(usersNotDeleted);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }


    // CREATE

    [HttpPost("Create")]
    public async Task<IActionResult> AddNewUser()
    {
        try
        {
            string file = await new StreamReader("bin/Debug/net7.0/CDriversDirs.txt").ReadToEndAsync();

            List<User>? users = JsonConvert.DeserializeObject<List<User>>(file);

            string body = await new StreamReader(Request.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body)) throw new Exception ("Body is empty or a white space");

            User? user = JsonConvert.DeserializeObject<User>(body);

            Guid uuid = Guid.NewGuid();
            string id = uuid.ToString();
            string? firstName = user.FirstName;
            string? lastName = user.LastName;
            string? username = user.Username;
            string? email = user.Email;
            bool deletedUser = false;

            if (
                string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email)
            )
            {
                throw new Exception("User data cannot be empty");
            }

            User newUser = new User(id, firstName, lastName, username, email, deletedUser);

            users.Add(newUser);

            string updateUsers = JsonConvert.SerializeObject(users);
            using (StreamWriter sw = new StreamWriter("bin/Debug/net7.0/CDriversDirs.txt"))
            {
                sw.WriteLine(updateUsers);
            }

            return Ok(newUser);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    // UPDATE

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateUser()
    {
        try
        {
            string file = await new StreamReader("bin/Debug/net7.0/CDriversDirs.txt").ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(file)) throw new Exception("There are no users yet");

            List<User>? users = JsonConvert.DeserializeObject<List<User>>(file);

            string body = await new StreamReader(Request.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body)) throw new Exception("Body is empty or a white space");

            User? user = JsonConvert.DeserializeObject<User>(body);

            string? id = user.Id;
            User? userToUpdate = users?.Find(user => user.Id == id);

            if (userToUpdate == null) throw new Exception("No user found with that id");

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Username = user.Username;
            userToUpdate.Email = user.Email;

            string updateUsers = JsonConvert.SerializeObject(users);
            using (StreamWriter sw = new StreamWriter("bin/Debug/net7.0/CDriversDirs.txt"))
            {
                sw.WriteLine(updateUsers);
            }

            return Ok(userToUpdate);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    // DELETE

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteUser()
    {
        try
        {
            string file = await new StreamReader("bin/Debug/net7.0/CDriversDirs.txt").ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(file)) throw new Exception("There are no users yet");

            List<User>? users = JsonConvert.DeserializeObject<List<User>>(file);

            string body = await new StreamReader(Request.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body)) throw new Exception("Body is empty or a white space");

            User? user = JsonConvert.DeserializeObject<User>(body);

            string? id = user.Id;
            User? userToDelete = users.Find(user => user.Id == id);

            if (userToDelete == null) throw new Exception("No user found with that id");

            userToDelete.DeletedUser = true;

            string updateUsers = JsonConvert.SerializeObject(users);
            using (StreamWriter sw = new StreamWriter("bin/Debug/net7.0/CDriversDirs.txt"))
            {
                sw.WriteLine(updateUsers);
            }

            return Ok(userToDelete);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
