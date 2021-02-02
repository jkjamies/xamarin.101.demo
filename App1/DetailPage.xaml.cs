using System;
using System.Collections.Generic;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(DetailPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
