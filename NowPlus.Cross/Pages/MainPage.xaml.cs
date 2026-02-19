using NowPlus.Cross.Models;
using NowPlus.Cross.PageModels;

namespace NowPlus.Cross.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}