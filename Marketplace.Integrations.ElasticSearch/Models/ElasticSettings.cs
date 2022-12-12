namespace Marketplace.Integrations.ElasticSearch.Models
{
    public class ElasticSettings
    {
        public string Url { get; set; }
        public string DefaultIndex { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Fingerprint { get; set; }
    }
}
