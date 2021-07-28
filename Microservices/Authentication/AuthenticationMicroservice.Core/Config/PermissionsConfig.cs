namespace AuthenticationMicroservice.Core.Config
{
    public static class PermissionsConfig
    {
        public static string Permission = "Permission";

        public static class Action
        {
            public const string FullAccess = "Permission.Action.FullAccess";
        }

        public static class Account
        {
            public const string CanRead = "Permission.Account.CanRead";
            public const string CanEdit = "Permission.Account.CanEdit";
            public const string CanAdd = "Permission.Account.CanAdd";
            public const string CanDelete = "Permission.Account.CanDelete";
        }
    }
}
