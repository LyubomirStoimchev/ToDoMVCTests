using System.Text.Json.Serialization;

namespace SpecFlowProject1.Configurations
{
    public class Timeouts
    {
        [JsonPropertyName("Visible")]
        public int Visible { get; set; }

        [JsonPropertyName("Exist")]
        public int Exist { get; set; }
    }
}