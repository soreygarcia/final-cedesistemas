using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Traveler.Models;
using Traveler.ViewModels.Contracts;
using Traveler.ViewModels.Domain;
using Xamarin.Forms;

namespace Traveler.ViewModels
{
	public class PlaceDetailPageViewModel : ViewModelBase
	{
        public PlaceModel SelectedPlace { get; set; }

        public ICommand OpenWazeCommand { get; set; }
        public ObservableCollection<ILocationViewModel> Items { get; set; }

        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OpenWazeCommand = new DelegateCommand(OpenWaze);
            Items = new ObservableCollection<ILocationViewModel>();
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
            Items.Add(new LocationItemViewModel()
            {
                Title = SelectedPlace.Name,
                Latitude = Convert.ToDouble(SelectedPlace.Latitude),
                Longitude = Convert.ToDouble(SelectedPlace.Longitude),
                Command = OpenWazeCommand
            });

            base.OnNavigatingTo(parameters);
        }
    }
}
