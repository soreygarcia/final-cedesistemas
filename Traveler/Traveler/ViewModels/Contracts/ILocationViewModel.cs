using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Traveler.ViewModels.Contracts
{
    public interface ILocationViewModel
    {
        string Title { get; set; }
        string Description { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        ICommand Command { get; set; }
    }
}
