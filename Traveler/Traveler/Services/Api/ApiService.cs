using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;
using Traveler.Resources;
using Traveler.Services.Network;

namespace Traveler.Services.Api
{
    public class ApiService : IApiService
    {
        private readonly INetworkService _networkService;

        public ApiService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<ContentResponse<PlaceModel>> CreatePlace(PlaceModel placeModel)
        {
            if (_networkService.IsConnected())
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = AppConfigurations.ApiRoot + AppConfigurations.GetAllPlacesEndpoint;

                    var body = new StringContent(JsonConvert.SerializeObject(placeModel), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(url, body);

                    string data = await result.Content.ReadAsStringAsync();

                    return new ContentResponse<PlaceModel>()
                    {
                        HttpResponse = result,
                        Data = JsonConvert.DeserializeObject<PlaceModel>(data)
                    };
                }
            }
            else
            {
                return new ContentResponse<PlaceModel>()
                {
                    HttpResponse = new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                };
            }
        }

        public async Task<ContentResponse<List<PlaceModel>>> GetAllPlaces()
        {
            if (_networkService.IsConnected())
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = AppConfigurations.ApiRoot + AppConfigurations.GetAllPlacesEndpoint;
                    var result = await client.GetAsync(url);
                    string data = await result.Content.ReadAsStringAsync();

                    return new ContentResponse<List<PlaceModel>>()
                    {
                        HttpResponse = result,
                        Data = JsonConvert.DeserializeObject<List<PlaceModel>>(data)
                    };
                }
            }
            else
            {
                return new ContentResponse<List<PlaceModel>>()
                {
                    HttpResponse = new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                };
            }
        }
    }
}
