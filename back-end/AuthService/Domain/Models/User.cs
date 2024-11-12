using AuthService.Domain.Utils;

namespace AuthService.Domain.Models
{
    public class User
    {
        #region Properties
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        #endregion

        #region Constructors
        private User() { }

        public User(long id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password.PasswordEncrypt("a1b2c3d4");
        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password.PasswordEncrypt("a1b2c3d4");
        }
        #endregion

        #region Methods
        public bool Validate() => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        #endregion
    }
}
