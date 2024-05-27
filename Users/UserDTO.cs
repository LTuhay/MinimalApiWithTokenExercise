namespace MinimalApiWithToken.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            (UserName, Password) = (user.UserName, user.Password);
        }

    }
}
