using System.ComponentModel.DataAnnotations;

namespace Lib.Data.Entities {

    public class IPDetails {

        [Key]
        public string Ip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string Continent { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string guid { get; set; }

        public bool? isProcessed { get; set; }
    }

}