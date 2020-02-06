
using Newtonsoft.Json;
using Lib.Service;

namespace Lib.Implementaion
{
    class IPDetails :IIPDetails
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country_name")]
        public string Country { get; set; }

        [JsonProperty("continent_name")]
        public string Continent { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
