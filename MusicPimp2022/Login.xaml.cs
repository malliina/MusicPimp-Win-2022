﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MusicPimp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        //public LibraryVM VM { get; set; }
        public PimpVM VM { get; set; }


        public Login()
        {
            VM = new PimpVM();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            VM.OnLoginSuccess += OnLoginSuccess;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            VM.OnLoginSuccess -= OnLoginSuccess;
            base.OnNavigatedFrom(e);
        }

        private void OnLoginSuccess(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(Library));
        }
    }
}
