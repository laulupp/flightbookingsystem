namespace Backend;

public class Constants
{
    public static class Headers
    {
        public const string Username = "X-API-Username";
        public const string Token = "X-API-Token";
    }

    public static class Items
    {
        public const string IsAdmin = "IsAdmin";
        public const string IsCompany = "IsCompany";
        public const string IsUser = "IsUser";
    }

    public static class Permissions
    {
        public const string Admin = "Admin";
        public const string CompanyRepresentative = "CompanyRepresentative";
        public const string Traveler = "Traveler";
    }
}
