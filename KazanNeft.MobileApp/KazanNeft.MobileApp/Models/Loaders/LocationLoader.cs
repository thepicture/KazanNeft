using KazanNeft.MobileApp.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace KazanNeft.MobileApp.Models.Loaders
{
    public class LocationLoader : ILoader<IEnumerable<Location>>
    {
        public IEnumerable<Location> Load()
        {
            List<Location> locations = new List<Location>
            {
                new Location { Name = "Asset Group" },
            };
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.LocationsUrl);
            locations.AddRange(JsonConvert.DeserializeObject<Location[]>(response));
            return locations;
        }
    }
}
