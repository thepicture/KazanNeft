using KazanNeft.MobileApp.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KazanNeft.MobileApp.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetCataloguePage : ContentPage
    {
        public AssetCataloguePage()
        {
            InitializeComponent();
            var departments = new List<Department>
            {
                new Department { Name = nameof(Department) },
            };
            var assetGroups = new List<AssetGroup>
            {
                new AssetGroup { Name = "Asset Group" },
            };
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.AssetsUrl);
            Asset[] assets = JsonConvert.DeserializeObject<Asset[]>(response);
            BindableLayout.SetItemsSource(AssetList, assets);
            AssetCount.Text = assets.Length + " assets from " + assets.Length;
            DepartmentPicker.ItemsSource = departments;
            AssetGroupPicker.ItemsSource = assetGroups;
            DepartmentPicker.SelectedIndex = 0;
            AssetGroupPicker.SelectedIndex = 0;
        }
    }
}