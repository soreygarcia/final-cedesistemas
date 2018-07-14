using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Traveler.Controls
{
    public class ExtendedMap : Map
    {
        public event EventHandler<TapEventArgs> Tap;
        public ExtendedMap() { }

        public ExtendedMap(MapSpan region) : base(region) { }

        public static readonly BindableProperty ZoomDistanceProperty =
            BindableProperty.Create("ZoomDistance", typeof(double), typeof(ExtendedMap), default(double));

        public double ZoomDistance
        {
            get { return (double)GetValue(ZoomDistanceProperty); }
            set { SetValue(ZoomDistanceProperty, value); }
        }

        public static readonly BindableProperty CurrentLocationProperty =
             BindableProperty.Create("CurrentLocation", typeof(Position), typeof(ExtendedMap), default(Position), propertyChanged: OnCurrentLocationPropertyChanged);

        public Position CurrentLocation
        {
            get { return (Position)GetValue(CurrentLocationProperty); }
            set { SetValue(CurrentLocationProperty, value); }
        }

        private static void OnCurrentLocationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as ExtendedMap;

            if (newValue != null)
            {
                Distance distance;

                if (map.ZoomDistance <= 0)
                    distance = Distance.FromKilometers(8);
                else
                    distance = Distance.FromKilometers(map.ZoomDistance);

                map.MoveToRegion(MapSpan.FromCenterAndRadius((Position)newValue, distance));
            }
        }

        public void OnTap(Position coordinate)
        {
            OnTap(new TapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(TapEventArgs e)
        {
            var handler = Tap;
            if (handler != null)
                handler(this, e);
        }
    }

    public class TapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}