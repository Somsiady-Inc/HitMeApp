namespace HitMeApp.Indentity.Core.Services
{
    internal interface IUserPasswordService
    {
        public string GeneratePasswordForUser(string password);
    }
}
