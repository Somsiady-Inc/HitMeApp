namespace HitMeApp.Indentity.Core.Services
{
    internal interface IUserPasswordService
    {
        public string GeneratePasswordForUser(string password);

        public bool Verify(string currentPassword, string newPassword);
    }
}
