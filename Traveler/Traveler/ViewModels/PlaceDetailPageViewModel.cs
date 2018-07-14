using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Traveler.Models;
using Xamarin.Forms;

namespace Traveler.ViewModels
{
	public class PlaceDetailPageViewModel : ViewModelBase
	{
        public PlaceModel SelectedPlace { get; set; }

        public ICommand OpenWazeCommand { get; set; }

        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OpenWazeCommand = new DelegateCommand(OpenWaze);
        }

        private void OpenWaze()
        {
            try
            {
                //TODO: Esto deberia ir en un servicio
                Device.OpenUri(new Uri($"https://waze.com/ul?ll={SelectedPlace.Latitude},{SelectedPlace.Longitude}&navigate=yes"));
            }
            catch
            {
                //if (Device.RuntimePlatform.Equals(Device.iOS))

                if (Device.RuntimePlatform.Equals(Device.Android))
                        Device.OpenUri(new Uri($"https://play.google.com/store/apps/details?id=com.waze"));
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SelectedPlace = (PlaceModel)parameters["SelectedPlace"];

            base.OnNavigatingTo(parameters);
        }
    }
}
