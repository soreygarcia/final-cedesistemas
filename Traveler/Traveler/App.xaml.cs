using Prism;
using Prism.Ioc;
using Traveler.ViewModels;
using Traveler.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using Traveler.Services.Api;
using Traveler.Services.Network;
using Traveler.Services.Dialogs;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Traveler
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<NewPlacePage>();

            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<INetworkService, NetworkService>();
            containerRegistry.Register<IDialogsService, DialogsService>();
        }
    }
}
