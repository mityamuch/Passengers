using Passengers.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Passengers.Services
{
    public sealed class JsonDataFormat : IDataFormat
    {
        public string FileFormatMask { get; set; } = "JSON files (*.json)|*.json|All files (*.*)|*.*";

        public List<Passenger> Deserialize(string data)
        {
            return JsonSerializer.Deserialize<List<Passenger>>(data);
        }

        public string Serialize(ObservableCollection<Passenger> passengers)
        {
            return JsonSerializer.Serialize(passengers);
        }
    }
}
