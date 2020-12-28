namespace HitMeApp.Users.Contract.Commands
{
    public class RegisterUser : IUserCommand
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
