using Passengers.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Passengers.Services
{
    public interface IDataFormat
    {
        List<Passenger> Deserialize(string data);
        string Serialize(ObservableCollection<Passenger> passengers);

        string FileFormatMask { get; set; }
    }
}
