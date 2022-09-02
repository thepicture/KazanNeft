using KazanNeft.MobileApp.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace KazanNeft.MobileApp.Models.Loaders
{
    public class AssetGroupLoader : ILoader<IEnumerable<AssetGroup>>
    {
        public IEnumerable<AssetGroup> Load()
        {
            List<AssetGroup> assetGroups = new List<AssetGroup>
            {
                new AssetGroup { Name = "Asset Group" },
            };
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.AssetsGroupsUrl);
            assetGroups.AddRange(JsonConvert.DeserializeObject<AssetGroup[]>(response));
            return assetGroups;
        }
    }
}
