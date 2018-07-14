using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ICommand SelectPlaceCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IDialogsService dialogsService, IApiService apiService)
            : base(navigationService)
        {
            Title = AppResources.ApplicationName;

            _dialogsService = dialogsService;
            _apiService = apiService;

            SelectPlaceCommand = new DelegateCommand<PlaceModel>
                (async (selectedPlace) => await SelectPlace(selectedPlace));
            NavigateCommand = new DelegateCommand<string>
                (async (pageKey) => await Navigate(pageKey));
        }

        private async Task Navigate(string pageKey)
        {
            await _navigationService.NavigateAsync(pageKey);
        }

        public ObservableCollection<PlaceModel> Places { get; set; } 

        private async Task SelectPlace(PlaceModel selectedPlace)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("SelectedPlace", selectedPlace);

            await _navigationService.NavigateAsync("PlaceDetailPage", parameters);
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            await LoadPlaces();
            base.OnNavigatingTo(parameters);
        }

        private async Task LoadPlaces()
        {
            try
            {
                _dialogsService.ShowDialog();

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
                    "Ha ocurrido un error inesperado. Verifique su conexion a internet.");
            }
            finally
            {
                _dialogsService.HideDialog();
            }
        }
    }
}
