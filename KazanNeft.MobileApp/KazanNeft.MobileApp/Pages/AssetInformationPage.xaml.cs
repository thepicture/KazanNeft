using KazanNeft.MobileApp.Models.Loaders;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KazanNeft.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetInformationPage : ContentPage
    {
        public AssetInformationPage()
        {
            InitializeComponent();

            LoadDepartments();
            LoadLocations();
            LoadAssetGroups();
            LoadAccountableParties();

            NavigationPage.SetHasBackButton(this, false);
        }

        private void LoadAccountableParties()
        {
            try
            {
                AccountablePartyPicker.ItemsSource = new AccountablePartyLoader()
                    .Load()
                    .ToList();
                AccountablePartyPicker.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void LoadAssetGroups()
        {
            try
            {
                AssetGroupPicker.ItemsSource = new AssetGroupLoader()
                    .Load()
                    .ToList();
                AssetGroupPicker.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void LoadLocations()
        {
            try
            {
                LocationPicker.ItemsSource = new LocationLoader()
                    .Load()
                    .ToList();
                LocationPicker.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void LoadDepartments()
        {
            try
            {
                DepartmentPicker.ItemsSource = new DepartmentLoader()
                    .Load()
                    .ToList();
                DepartmentPicker.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void OnNavigateBack(object sender, EventArgs e)
        {
            if (await DisplayAlert("Question", "Do you really want to cancel and navigate back?", "Yes", "No"))
            {
                await Navigation.PopAsync();
            }
        }
    }
}