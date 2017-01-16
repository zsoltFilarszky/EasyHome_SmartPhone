using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace EasyHome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string _serverIp;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void  MainPageOnLoad(object sender, RoutedEventArgs e)
        {
            connectionStatus.Foreground = new SolidColorBrush(Colors.DarkRed);
            connectionStatus.Text = "Not Connected!";

            _serverIp = ipTextBox.Text;     
            var pingResult = await PingWebserver();
            if (pingResult == HttpStatusCode.OK)
            {
                connectionStatus.Foreground = new SolidColorBrush(Colors.Green);
                connectionStatus.Text = "Connected";
            }
        }

        private async Task<HttpStatusCode> PingWebserver()
        {
            using (HttpClient client = new HttpClient())
            {
                string fullUrlPath = String.Format("http://{0}/EasyAutomation/index.html", _serverIp);
                HttpResponseMessage responeMessage = await client.GetAsync(fullUrlPath);
                return responeMessage.StatusCode;
            }
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EasyHome),_serverIp);
        }
    }
}
