namespace AuthServer.Controllers
{
    public class Audience
    {
        public string? Secret { get; set; }

        public string? Issuer { get; set; }

        public string? AudienceParameter { get; set; }
    }
}
