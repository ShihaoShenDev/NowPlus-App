using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Dispatching;

namespace NowPlus.Cross.PageModels
{
    public partial class ClockPageModel : ObservableObject
    {
        [ObservableProperty]
        private string _currentTime = string.Empty;

        [ObservableProperty]
        private string _currentDate = string.Empty;

        private readonly IDispatcherTimer _timer;

        public ClockPageModel(IDispatcher dispatcher)
        {
            _timer = dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) => UpdateTime();
            _timer.Start();

            UpdateTime();
        }

        private void UpdateTime()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            CurrentDate = DateTime.Now.ToString("dddd, MMMM d, yyyy");
        }
    }
}
