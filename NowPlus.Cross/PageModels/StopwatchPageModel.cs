using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Dispatching;

namespace NowPlus.Cross.PageModels
{
    public partial class StopwatchPageModel : ObservableObject
    {
        private readonly Stopwatch _stopwatch;
        private readonly IDispatcherTimer _timer;

        [ObservableProperty]
        private string _elapsedTime = "00:00:00.00";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StartStopText))]
        private bool _isRunning;

        [ObservableProperty]
        private ObservableCollection<string> _laps = new();

        public string StartStopText => IsRunning ? "Pause" : "Start";

        public StopwatchPageModel(IDispatcher dispatcher)
        {
            _stopwatch = new Stopwatch();
            _timer = dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += (s, e) => UpdateElapsedTime();
        }

        private void UpdateElapsedTime()
        {
            ElapsedTime = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.ff");
        }

        [RelayCommand]
        private void StartStop()
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
                _timer.Stop();
                IsRunning = false;
            }
            else
            {
                _stopwatch.Start();
                _timer.Start();
                IsRunning = true;
            }
        }

        [RelayCommand]
        private void Reset()
        {
            _stopwatch.Reset();
            _timer.Stop();
            IsRunning = false;
            ElapsedTime = "00:00:00.00";
            Laps.Clear();
        }

        [RelayCommand]
        private void Lap()
        {
            if (_stopwatch.IsRunning)
            {
                Laps.Insert(0, $"Lap {Laps.Count + 1}: {ElapsedTime}");
            }
        }
    }
}
