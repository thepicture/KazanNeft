using KazanNeft.MobileApp.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace KazanNeft.MobileApp.Models.Loaders
{
    public class AccountablePartyLoader : ILoader<IEnumerable<AccountableParty>>
    {
        public IEnumerable<AccountableParty> Load()
        {
            List<AccountableParty> accountableParties = new List<AccountableParty>
            {
                new AccountableParty { LastName = "Accountable", FirstName="Party" },
            };
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.AccountablePartiesUrl);
            accountableParties.AddRange(JsonConvert.DeserializeObject<AccountableParty[]>(response));
            return accountableParties;
        }
    }
}
