using NowPlus.Cross.PageModels;

namespace NowPlus.Cross.Pages
{
    public partial class StopwatchPage : ContentPage
    {
        public StopwatchPage(StopwatchPageModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
