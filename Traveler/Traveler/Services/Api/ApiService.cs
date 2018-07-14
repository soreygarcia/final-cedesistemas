using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;

namespace Traveler.Services.Api
{
    public class ApiService : IApiService
    {
        public Task<ContentResponse<PlaceModel>> CreatePlace()
        {
            throw new NotImplementedException();
        }

        public Task<ContentResponse<List<PlaceModel>>> GetAllPlaces()
        {
            throw new NotImplementedException();
        }
    }
}
