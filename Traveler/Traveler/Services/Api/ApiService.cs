using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;
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

        public async Task<ContentResponse<PlaceModel>> CreatePlace()
        {
            if (_networkService.IsConnected())
            {
                using (HttpClient client = new HttpClient())
                {

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
                    var result = await client.GetAsync("");
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
