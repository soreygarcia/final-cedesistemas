using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Traveler.ViewModels.Contracts;

namespace Traveler.ViewModels.Domain
{
    public class LocationItemViewModel : ILocationViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICommand Command { get; set; }
    }
}
