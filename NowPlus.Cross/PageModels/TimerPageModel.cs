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
        [NotifyPropertyChangedFor(nameof(RemainingTimeDisplay))]
        private string _remainingTime = "00:00:00";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainingTimeDisplay))]
        private int _hours;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainingTimeDisplay))]
        private int _minutes;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RemainingTimeDisplay))]
        private int _seconds;

        public string RemainingTimeDisplay 
        {
            get 
            {
                if (IsRunning || _remainingTimeSpan.TotalSeconds > 0)
                    return _remainingTime;
                
                return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StartStopText))]
        [NotifyPropertyChangedFor(nameof(IsNotRunning))]
        [NotifyPropertyChangedFor(nameof(RemainingTimeDisplay))]
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
            OnPropertyChanged(nameof(RemainingTimeDisplay));
        }

        [RelayCommand]
        private void AdjustHours(string delta)
        {
            if (int.TryParse(delta, out int d))
                Hours = Math.Max(0, Math.Min(99, Hours + d));
        }

        [RelayCommand]
        private void AdjustMinutes(string delta)
        {
            if (int.TryParse(delta, out int d))
            {
                var newValue = Minutes + d;
                if (newValue > 59) Minutes = 0;
                else if (newValue < 0) Minutes = 59;
                else Minutes = newValue;
            }
        }

        [RelayCommand]
        private void AdjustSeconds(string delta)
        {
            if (int.TryParse(delta, out int d))
            {
                var newValue = Seconds + d;
                if (newValue > 59) Seconds = 0;
                else if (newValue < 0) Seconds = 59;
                else Seconds = newValue;
            }
        }
    }
}
