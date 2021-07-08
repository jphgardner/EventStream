namespace EventStream.Domain.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public User(string username, string password)
        {
            Username = username.ToLower();
            Salt = Domain.Password.CreateRandomSalt();
            var passwordObj = new Password(password, Salt);
            Password = passwordObj.Resolve();
        }
    }
}