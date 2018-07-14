using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;

namespace Traveler.Services.Api
{
    public interface IApiService
    {
        Task<ContentResponse<List<PlaceModel>>> GetAllPlaces();
        Task<ContentResponse<PlaceModel>> CreatePlace(PlaceModel placeModel);
    }
}
