using NowPlus.Cross.PageModels;

namespace NowPlus.Cross.Pages
{
    public partial class ClockPage : ContentPage
    {
        public ClockPage(ClockPageModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
