using NowPlus.Cross.PageModels;

namespace NowPlus.Cross.Pages
{
    public partial class TimerPage : ContentPage
    {
        public TimerPage(TimerPageModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
