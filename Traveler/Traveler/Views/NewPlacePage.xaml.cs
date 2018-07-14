using Traveler.Controls;
using Traveler.ViewModels;
using Traveler.ViewModels.Domain;
using Xamarin.Forms;

namespace Traveler.Views
{
    public partial class NewPlacePage : ContentPage
    {
        public NewPlacePage()
        {
            InitializeComponent();
        }

        private void MainPage_Tap(object sender, TapEventArgs e)
        {
            var context = (NewPlacePageViewModel)this.BindingContext;
            context.DrawLocation(new LocationItemViewModel()
            {
                Title = "Desconocido",
                Latitude = e.Position.Latitude,
                Longitude = e.Position.Longitude
            });
        }
    }
}
