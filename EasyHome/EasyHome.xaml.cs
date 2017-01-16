using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EasyHome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EasyHome : Page
    {
        private string _ipAddress;
        public EasyHome()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _ipAddress = e.Parameter as string;
        }

        private void MySensor_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MySensors), new ApiHandler(String.Format("http://{0}",_ipAddress)));
        }

        private void Statistics_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReportingPage), new ApiHandler(String.Format("http://{0}", _ipAddress)));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           Application.Current.Exit();
        }
    }
}
