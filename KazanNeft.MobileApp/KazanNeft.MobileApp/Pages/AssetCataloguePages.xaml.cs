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

            LoadDepartmentsToPicker();
            LoadAssetGroupsToPicker();

            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.AssetsUrl);
            Asset[] assets = JsonConvert.DeserializeObject<Asset[]>(response);
            BindableLayout.SetItemsSource(AssetList, assets);
            AssetCount.Text = assets.Length + " assets from " + assets.Length;
        }

        private void LoadAssetGroupsToPicker()
        {
            List<AssetGroup> assetGroups = new List<AssetGroup>
            {
                new AssetGroup { Name = "Asset Group" },
            };
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.AssetsGroupsUrl);
            assetGroups.AddRange(JsonConvert.DeserializeObject<AssetGroup[]>(response));
            AssetGroupPicker.ItemsSource = assetGroups;
            AssetGroupPicker.SelectedIndex = 0;
        }

        private void LoadDepartmentsToPicker()
        {
            List<Department> departments = new List<Department>
            {
                new Department { Name = nameof(Department) },
            };
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.DepartmentsUrl);
            departments.AddRange(JsonConvert.DeserializeObject<Department[]>(response));
            DepartmentPicker.ItemsSource = departments;
            DepartmentPicker.SelectedIndex = 0;
        }
    }
}