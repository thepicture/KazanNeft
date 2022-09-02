using KazanNeft.MobileApp.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace KazanNeft.MobileApp.Models.Loaders
{
    public class DepartmentLoader : ILoader<IEnumerable<Department>>
    {
        public IEnumerable<Department> Load()
        {
            List<Department> departments = new List<Department>
            {
                new Department { Name = nameof(Department) },
            };
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.DepartmentsUrl);
            departments.AddRange(JsonConvert.DeserializeObject<Department[]>(response));
            return departments;
        }
    }
}
