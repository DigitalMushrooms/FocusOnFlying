namespace FocusOnFlying.Application.Common.Interfaces
{
    public interface IAppSettingsService
    {
        public string FocusOnFlyingConnectionString { get; }
        public string IdentityProviderAddress { get; }
        public string IdentityProviderUsersPath { get; }
    }
}
