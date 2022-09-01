
using KazanNeft.MobileApp.Models.Entities;
using System.Collections.Generic;
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
                new Department {Name = nameof(Department)},
            };
            var assetGroups = new List<AssetGroup>
            {
                new AssetGroup {Name = nameof(AssetGroup).Replace("tG", "t G")},
            };
            DepartmentPicker.ItemsSource = departments;
            AssetGroupPicker.ItemsSource = assetGroups;
            DepartmentPicker.SelectedIndex = 0;
            AssetGroupPicker.SelectedIndex = 0;
        }
    }
}