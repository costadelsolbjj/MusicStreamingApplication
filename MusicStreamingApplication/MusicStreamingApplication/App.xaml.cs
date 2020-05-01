using System;
using MusicStreamingApplication.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MusicStreamingApplication.Services;
using MusicStreamingApplication.Views;

namespace MusicStreamingApplication
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
         //DeviceInfo.Platform == DevicePlatform.Android ? "https://192.168.1.141:5002/" : "http://localhost:5000";
        //DeviceInfo.Platform == DevicePlatform.Android? "http://192.168.1.141:5002/" : "http://localhost:5000";
        //DeviceInfo.Platform == DevicePlatform.Android? "http://127.0.0.1:5000/" : "http://localhost:5000";
        
        DeviceInfo.Platform == DevicePlatform.Android? "https://musicstreamingmg.azurewebsites.net" : "http://localhost:5000";
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MusicPage());

            //if (UseMockDataStore)
            //    DependencyService.Register<MockDataStore>();
            //else
            //    DependencyService.Register<AzureDataStore>();
            //MainPage = new MainPage();
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
