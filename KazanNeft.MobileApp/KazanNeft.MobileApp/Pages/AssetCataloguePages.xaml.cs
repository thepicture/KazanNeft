using KazanNeft.MobileApp.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            EndDate.Date = DateTime.Now + TimeSpan.FromDays(365);
            StartDate.Date = DateTime.Now - TimeSpan.FromDays(365);

            FilterAssets();
        }

        private void FilterAssets()
        {
            WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string response = client.DownloadString(Api.AssetsUrl);
            IEnumerable<Asset> assets = JsonConvert.DeserializeObject<Asset[]>(response);
            int totalAssetsCount = assets.Count();

            if (DepartmentPicker.SelectedIndex > 0)
            {
                assets = assets.Where(a => a.DepartmentID == (DepartmentPicker.SelectedItem as Department).ID);
            }

            if (AssetGroupPicker.SelectedIndex > 0)
            {
                assets = assets.Where(a => a.AssetGroupID == (AssetGroupPicker.SelectedItem as AssetGroup).ID);
            }

            if (!string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                assets = assets.Where(a =>
                {
                    return a.AssetSN.IndexOf(SearchBox.Text, StringComparison.OrdinalIgnoreCase) != -1
                           || a.AssetName.IndexOf(SearchBox.Text, StringComparison.OrdinalIgnoreCase) != -1;
                });
            }

            if (StartDate.Date != null && EndDate.Date != null && StartDate.Date <= EndDate.Date)
            {
                assets = assets.Where(a => a.WarrantyDate == null || a.WarrantyDate <= EndDate.Date && a.WarrantyDate >= StartDate.Date);
            }

            BindableLayout.SetItemsSource(AssetList, assets);
            AssetCount.Text = assets.Count() + " assets from " + totalAssetsCount;
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

        private void OnDepartmentUnfocused(object sender, FocusEventArgs e)
        {
            FilterAssets();
        }

        private void OnAssetGroupUnfocused(object sender, FocusEventArgs e)
        {
            FilterAssets();
        }

        private void OnSearchBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text.Length > 2)
            {
                FilterAssets();
            }
        }

        private void OnStartDateSelected(object sender, DateChangedEventArgs e)
        {
            FilterAssets();
        }

        private void OnEndDateSelected(object sender, DateChangedEventArgs e)
        {
            FilterAssets();
        }
    }
}