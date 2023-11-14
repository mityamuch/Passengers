using System;
using Wpf.MVVM.Core;

namespace Passengers.Models
{
    public sealed class Passenger : ViewModelBase
    {
        private string _flightNumber;
        private DateTime _departureTime;
        private string _fullName;

        public string FlightNumber
        {
            get => _flightNumber;
            set
            {
                _flightNumber = value;
                OnPropertyChanged(nameof(FlightNumber));
            }
        }

        public DateTime DepartureTime
        {
            get => _departureTime;
            set
            {
                _departureTime = value;
                OnPropertyChanged(nameof(DepartureTime));
            }
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
    }
}
