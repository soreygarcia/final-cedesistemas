using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Traveler.Models
{
    public class ContentResponse<T> where T : class
    {
        public HttpResponseMessage HttpResponse { get; set; }
        public  T Data { get; set; }
    }
}
