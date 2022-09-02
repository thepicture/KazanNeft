using KazanNeft.MobileApp.Forms;
using Xamarin.Forms;

[assembly: ExportFont("Helvetica.ttc", Alias = "Helvetica")]
[assembly: ExportFont("Helvetica-Normal.ttf", Alias = "HelveticaNormal")]
[assembly: ExportFont("MyriadPro-Regular.otf", Alias = "MyriadProRegular")]
namespace KazanNeft.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AssetCataloguePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
