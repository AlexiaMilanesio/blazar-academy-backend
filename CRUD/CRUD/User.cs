using System;

namespace CRUD
{
    public class User
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool DeletedUser { get; set; }

        public User(string id, string? firstName, string? lastName, string? userName, string? email, bool deletedUser)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = userName;
            Email = email;
            DeletedUser = deletedUser;
        }

        private User() { }
    }
}
