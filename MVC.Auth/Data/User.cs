namespace MVC.Auth.Data
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty; 
    }

    public static class FakeDbContext
    {
        public static List<User> Users = new List<User>
 {
 new User { Email = "admin@store.com", Password = "Password123!", FullName =
"System Admin" }
 };
    }

}
