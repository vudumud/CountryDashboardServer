using System;

namespace CountryDashboardServer.Components.Models
{
    public class Countries
    {
        public Guid ID { get; set; }
        public string Country { get; set; }
        public string IsoCode { get; set; }
    }
}

