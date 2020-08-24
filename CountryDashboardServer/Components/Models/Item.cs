using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDashboardServer.Components.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Iso2Code { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
        public Adminregion Adminregion { get; set; }
        public IncomeLevel IncomeLevel { get; set; }
        public LendingType LendingType { get; set; }
        public string CapitalCity { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public PagesDetails PagesDetails { get; set; }
    }
}
