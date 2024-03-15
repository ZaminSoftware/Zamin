namespace Zamin.Extensions.UsersManagement.Abstractions;

public interface IUserInfoService
{
    string GetUserId();
    string GetUserName();
    string GetFirstName();
    string GetLastName();
    string UserAgent();
    string GetUserIp();
    string GetClientId();
    bool TrueIfCurrentUser(string userId);
    string? GetClaim(string claimType);
    [Obsolete("UserIdOrDefault will be remove in next version")]
    string UserIdOrDefault();
    [Obsolete("UserIdOrDefault will be remove in next version")]
    string UserIdOrDefault(string defaultValue);
}