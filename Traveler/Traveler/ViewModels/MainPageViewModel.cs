using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;
using Traveler.Resources;

using Traveler.Services.Api;
using Traveler.Services.Dialogs;

namespace Traveler.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IDialogsService _dialogsService;
        private readonly IApiService _apiService;

        public MainPageViewModel(INavigationService navigationService, IDialogsService dialogsService, IApiService apiService)
            : base(navigationService)
        {
            Title = AppResources.ApplicationName;

            _dialogsService = dialogsService;
            _apiService = apiService;
        }

        public ObservableCollection<PlaceModel> Places { get; set; }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            await LoadPlaces();
            base.OnNavigatingTo(parameters);
        }

        private async Task LoadPlaces()
        {
            try
            {
                var result = await _apiService.GetAllPlaces();
                if (result.HttpResponse.IsSuccessStatusCode)
                {
                    Places = new ObservableCollection<PlaceModel>(result.Data);
                }
                else
                {
                    await _dialogsService.ShowMessage(AppResources.ApplicationName, 
                        "Ha ocurrido un error inesperado.");
                }
            }
            catch (Exception)
            {
                await _dialogsService.ShowMessage(AppResources.ApplicationName, 
                    "Ha ocurrido un error inesperado.");
            }
        }
    }
}
