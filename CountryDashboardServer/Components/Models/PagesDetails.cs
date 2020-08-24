using CountryDashboardServer.Components.Models;

namespace CountryDashboardServer.Components.Models
{
    public class PagesDetails
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public string Per_page { get; set; }
        public int Total { get; set; }
    }   
}