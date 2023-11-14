using Microsoft.Win32;
using Passengers.Models;
using Passengers.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf.MVVM.Core;

namespace Passengers.ViewModels
{
    public sealed class FlightViewModel : ViewModelBase
    {
        public ObservableCollection<Passenger> Passengers { get; }

        #region Fields
        private PassengerDataService _dataService;
        private string _filepath = "passengers.json";
        #endregion

        #region Commands
        private ICommand? _saveDataCommand;
        private ICommand? _loadDataCommand;

        public ICommand SaveDataCommand =>
            _saveDataCommand ??= new RelayCommand(async _ => await SaveDataAsync());
        public ICommand LoadDataCommand =>
            _loadDataCommand ??= new RelayCommand(async _ => await OpenFileAsync());
        #endregion

        public FlightViewModel()
        {
            JsonDataFormat jsonDataFormat = new JsonDataFormat();

            _dataService = new PassengerDataService(jsonDataFormat);
            Passengers = new ObservableCollection<Passenger>();
            Passengers.CollectionChanged += Passengers_CollectionChanged;
        }
        private async void Passengers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await SaveDataAsync();
        }

        private async Task OpenFileAsync(CancellationToken token = default)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = _dataService.FileFormatMask;
            if (openFileDialog.ShowDialog() == true)
            {
                _filepath = openFileDialog.FileName;
                await LoadDataFromFile(token);
            }
        }

        private async Task LoadDataFromFile(CancellationToken token = default)
        {
            try
            {
                var passengers = await _dataService.LoadDataAsync(_filepath, token);
                Passengers.Clear();
                passengers.ForEach(p => Passengers.Add(p));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task SaveDataAsync(CancellationToken token = default)
        {
            try
            {
                await _dataService.SaveDataAsync(_filepath, Passengers, token);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
