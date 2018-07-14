using Plugin.Geolocator;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Traveler.Resources;
using Traveler.Services.Api;
using Traveler.Services.Dialogs;
using Traveler.ViewModels.Contracts;
using Traveler.ViewModels.Domain;

namespace Traveler.ViewModels
{
	public class NewPlacePageViewModel : ViewModelBase
	{

        private readonly IDialogsService _dialogsService;
        private readonly IApiService _apiService;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public ICommand CreateCommand { get; set; }
        public ObservableCollection<ILocationViewModel> Items { get; set; }

        public NewPlacePageViewModel(INavigationService navigationService, IDialogsService dialogsService, IApiService apiService) : base(navigationService)
        {
            _dialogsService = dialogsService;
            _apiService = apiService;

            CreateCommand = new DelegateCommand(async () => await Create());
            Items = new ObservableCollection<ILocationViewModel>();
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private async Task Create()
        {
            try
            {
                _dialogsService.ShowDialog();

                var result = await _apiService.CreatePlace(new Models.PlaceModel()
                {
                    Name = this.Name,
                    Description = this.Description,
                    Longitude = this.Longitude,
                    Latitude = this.Latitude
                });

                if (result.HttpResponse.IsSuccessStatusCode)
                {
                    await _dialogsService.ShowMessage(AppResources.ApplicationName,
                        "Nuevo lugar guardado");
                    await _navigationService.GoBackAsync();
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

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(Items));
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Latitude = position.Latitude.ToString();
            Longitude = position.Longitude.ToString();

            DrawLocation(new LocationItemViewModel()
            {
                Title = "Mi posición actual",
                Latitude = position.Latitude,
                Longitude = position.Longitude
            });

            base.OnNavigatingTo(parameters);
        }

        internal void DrawLocation(LocationItemViewModel locationItemViewModel)
        {
            var itemsTemp = new ObservableCollection<ILocationViewModel>();
            itemsTemp.Add(locationItemViewModel);
            Items = itemsTemp;
        }
    }
}
