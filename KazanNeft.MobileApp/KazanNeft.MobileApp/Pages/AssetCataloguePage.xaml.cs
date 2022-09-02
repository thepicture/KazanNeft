using KazanNeft.MobileApp.Models.Entities;
using KazanNeft.MobileApp.Models.Loaders;
using KazanNeft.MobileApp.Pages;
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
            AssetGroupPicker.ItemsSource = new AssetGroupLoader()
                .Load()
                .ToList();
            AssetGroupPicker.SelectedIndex = 0;
        }

        private void LoadDepartmentsToPicker()
        {
            DepartmentPicker.ItemsSource = new DepartmentLoader()
                .Load()
                .ToList();
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

        private async void OnNavigateToAddNewAsset(object sender, EventArgs e)
        {
            AssetInformationPage page = new AssetInformationPage();
            await Navigation.PushAsync(page);
        }
    }
}