namespace AliyewShop.Application.Shared;

public static class Permissions
{
    public static class Category
    {
        public const string Create = "Category.Create";
        public const string Update = "Category.Update";
        public const string Delete = "Category.Delete";

        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete
        };
    }
    public static class Role
    {
        public const string GetAllPermissions = "GetAllPermissions";
        public static List<string> All => new List<string>
        {
            GetAllPermissions
        };
    }
    public static class Account
    {
        public const string AddRole = "AddRole";



        public static List<string> All => new List<string>
        {
            AddRole
        };
    }
}
