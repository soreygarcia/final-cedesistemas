using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Traveler.Models;

namespace Traveler.ViewModels
{
	public class PlaceDetailPageViewModel : ViewModelBase
	{
        public PlaceModel SelectedPlace { get; set; }

        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            SelectedPlace = (PlaceModel)parameters["SelectedPlace"];

            base.OnNavigatingTo(parameters);
        }
    }
}
