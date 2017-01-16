using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
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
    public sealed partial class CreateSensorPage : Page
    {
        private ApiHandler _apiHandler;
        public CreateSensorPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _apiHandler=e.Parameter as ApiHandler;
        }

        private void GridOnLoad(object sender, RoutedEventArgs e)
        {
            
        }

        private async void CreateSensor_OnClick(object sender, RoutedEventArgs e)
        {
            string sensorName = _SensorName.Text;
            Changable isChangable;

            if (_ChangableCheckBox.IsChecked != null && _ChangableCheckBox.IsChecked.Value)
            {
                isChangable=Changable.True;
            }
            else
            {
                isChangable=Changable.False;
            }

            Sensor newSensor = new Sensor(0,sensorName,isChangable);
            var result = await _apiHandler.CreateSensor(newSensor);

            MessageDialog msgDialog = new MessageDialog("Sensor was created!");
            msgDialog.Commands.Add(new UICommand("OK"));
            if (result.Answer != null)
            {
                var msgDialogResult= await msgDialog.ShowAsync();        
                if (msgDialogResult.Label == "OK")
                {
                    Frame.GoBack();
                }
            }

        }
    }
}
