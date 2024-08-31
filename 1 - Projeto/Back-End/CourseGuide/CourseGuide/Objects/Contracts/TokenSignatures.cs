namespace CourseGuide.Objects.Contracts
{
    public class TokenSignatures
    {
        public string Issuer { get; } = "Course Guide API";
        public string Audience { get; } = "Course Guide WebSite";
        public string Key { get; } = "CourseGuide_Barrament_API_Autentication";
    }
}