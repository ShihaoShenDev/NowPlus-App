using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Dispatching;

namespace NowPlus.Cross.PageModels
{
    public partial class TimerPageModel : ObservableObject
    {
        private readonly IDispatcherTimer _timer;
        private TimeSpan _remainingTimeSpan;

        [ObservableProperty]
        private string _remainingTime = "00:00:00";

        [ObservableProperty]
        private int _hours;

        [ObservableProperty]
        private int _minutes;

        [ObservableProperty]
        private int _seconds;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StartStopText))]
        [NotifyPropertyChangedFor(nameof(IsNotRunning))]
        private bool _isRunning;

        public string StartStopText => IsRunning ? "Pause" : "Start";
        public bool IsNotRunning => !IsRunning;

        public TimerPageModel(IDispatcher dispatcher)
        {
            _timer = dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) => Tick();
        }

        private async void Tick()
        {
            if (_remainingTimeSpan.TotalSeconds > 0)
            {
                _remainingTimeSpan = _remainingTimeSpan.Subtract(TimeSpan.FromSeconds(1));
                RemainingTime = _remainingTimeSpan.ToString(@"hh\:mm\:ss");
            }
            else
            {
                _timer.Stop();
                IsRunning = false;
                RemainingTime = "00:00:00";
                await Shell.Current.DisplayAlertAsync("Timer", "Time's up!", "OK");
            }
        }

        [RelayCommand]
        private void StartStop()
        {
            if (IsRunning)
            {
                _timer.Stop();
                IsRunning = false;
            }
            else
            {
                if (_remainingTimeSpan.TotalSeconds <= 0)
                {
                    _remainingTimeSpan = new TimeSpan(Hours, Minutes, Seconds);
                    if (_remainingTimeSpan.TotalSeconds <= 0) return;
                }

                RemainingTime = _remainingTimeSpan.ToString(@"hh\:mm\:ss");
                _timer.Start();
                IsRunning = true;
            }
        }

        [RelayCommand]
        private void Reset()
        {
            _timer.Stop();
            IsRunning = false;
            _remainingTimeSpan = TimeSpan.Zero;
            RemainingTime = "00:00:00";
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }
    }
}
