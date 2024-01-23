namespace GameHuntWeb.Utility
{
    public class SD
    {
        public static string SubscriptionAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }
        public static string RecommendationAPIBase { get; set; }


        public const string RoleAdmin = "ADMIN";
        public const string RoleClient = "CLIENT";
        public const string RoleDeveloper = "DEVELOPER";
        public const string TokenCookie = "JWTToken";

        public enum ApiType { GET, POST, PUT, DELETE }
    }
}
