using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.Services.Network
{
    public class NetworkService : INetworkService
    {
        public bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
