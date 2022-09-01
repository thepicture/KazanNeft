
using KazanNeft.MobileApp.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KazanNeft.MobileApp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetCataloguePage : ContentPage
    {
        public readonly string artificialResponse = @"[{""ID"":1,""AssetName"":""Toyota Hilux FAF321"",""AssetSN"":""05/04/0001"",""DepartmentName"":""Distribution"",""AssetGroupName"":""Mechanical "",""WarrantyDate"":null},{""ID"":2,""AssetName"":""Suction Line 852"",""AssetSN"":""04/03/0001"",""DepartmentName"":""R&D"",""AssetGroupName"":""Electrical"",""WarrantyDate"":""2020-03-02T00:00:00""},{""ID"":3,""AssetName"":""ZENY 3,5CFM Single-Stage 5 Pa Rotary Vane"",""AssetSN"":""01/01/0001"",""DepartmentName"":""Exploration"",""AssetGroupName"":""Hydraulic"",""WarrantyDate"":""2018-01-17T00:00:00""},{""ID"":4,""AssetName"":""Volvo FH16"",""AssetSN"":""05/04/0002"",""DepartmentName"":""Distribution"",""AssetGroupName"":""Mechanical "",""WarrantyDate"":null}]";

        public AssetCataloguePage()
        {
            InitializeComponent();
            var departments = new List<Department>
            {
                new Department {Name = nameof(Department)},
            };
            var assetGroups = new List<AssetGroup>
            {
                new AssetGroup {Name = nameof(AssetGroup).Replace("tG", "t G")},
            };
            BindableLayout.SetItemsSource(AssetList,
                JsonConvert.DeserializeObject<Asset[]>(artificialResponse));
            AssetCount.Text = BindableLayout.GetItemsSource(AssetList).Cast<Asset>().Count()
                + " assets from "
                + BindableLayout.GetItemsSource(AssetList).Cast<Asset>().Count();
            DepartmentPicker.ItemsSource = departments;
            AssetGroupPicker.ItemsSource = assetGroups;
            DepartmentPicker.SelectedIndex = 0;
            AssetGroupPicker.SelectedIndex = 0;
        }
    }
}