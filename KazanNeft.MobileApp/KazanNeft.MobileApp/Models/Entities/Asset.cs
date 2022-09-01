using System;
using System.Collections.Generic;
using System.Text;

namespace KazanNeft.MobileApp.Models.Entities
{
    public class Asset
    {
        public int ID { get; set; }
        public string AssetName { get; set; }
        public string AssetSN { get; set; }
        public string DepartmentName { get; set; }
        public string AssetGroupName { get; set; }
        public DateTime? WarrantyDate { get; set; }
    }
}
