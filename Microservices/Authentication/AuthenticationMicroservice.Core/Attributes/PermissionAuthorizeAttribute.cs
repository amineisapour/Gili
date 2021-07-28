using Microsoft.AspNetCore.Authorization;

namespace AuthenticationMicroservice.Core.Attributes
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "PERMISSION:";
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            System.Array.Resize(ref permissions, permissions.Length + 1);
            permissions[^1] = Config.PermissionsConfig.Action.FullAccess;

            Policy = $"{PolicyPrefix}{string.Join(",", permissions)}";
        }
    }
}
