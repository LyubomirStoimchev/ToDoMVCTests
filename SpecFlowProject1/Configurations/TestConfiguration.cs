using System.Text.Json.Serialization;

namespace SpecFlowProject1.Configurations
{
    public class TestConfiguration
    {
        [JsonPropertyName("SiteUrl")] 
        public string SiteUrl { get; set; }

        [JsonPropertyName("Timeouts")] 
        public Timeouts Timeouts { get; set; }
    }
}