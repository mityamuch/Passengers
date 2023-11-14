using Passengers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Passengers.Services
{
    public sealed class PassengerDataService
    {
        private readonly IDataFormat _dataFormat;
        public string FileFormatMask { get; set; }
        public PassengerDataService(IDataFormat dataFormat)
        {
            _dataFormat = dataFormat ?? throw new ArgumentNullException(nameof(dataFormat));
            FileFormatMask = dataFormat.FileFormatMask;
        }

        public async Task<List<Passenger>> LoadDataAsync(string filePath, CancellationToken token = default)
        {
            string data = await File.ReadAllTextAsync(filePath, token);
            return _dataFormat.Deserialize(data);
        }

        public async Task SaveDataAsync(string filePath, ObservableCollection<Passenger> passengers, CancellationToken token = default)
        {
            string data = _dataFormat.Serialize(passengers);
            await File.WriteAllTextAsync(filePath, data, token);
        }
    }
}
