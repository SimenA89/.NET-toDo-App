using Microsoft.Extensions.DependencyInjection;
using System;

namespace ToDo
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetService<NavigationPage>();
        }
    }


}


