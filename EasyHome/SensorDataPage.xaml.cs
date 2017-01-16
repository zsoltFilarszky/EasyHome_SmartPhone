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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EasyHome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SensorDataPage : Page
    {
        private Sensor _sensor;
        private ApiHandler _apiHandler;
        public SensorDataPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PageParameterPayload payload = e.Parameter as PageParameterPayload;
            _sensor = payload.SensorParam;
            _apiHandler = payload.ApiHandlerParam;

        }

        private async void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var sensorDataList = await _apiHandler.GetLatestSensorData(_sensor.Id);
       
            if (sensorDataList != null)
            {
                _SensorName.Text = _sensor.Type;
                _UnitTextBox.Text = sensorDataList[0].Unit;
                _TimeTextBox.Text = sensorDataList[0].Time;
                _ValueTextBox.Text = sensorDataList[0].Value;
            }
            else
            {
                _SensorName.Text = _sensor.Type;
                _UnitTextBox.Text = "NO VALUE";
                _TimeTextBox.Text = "NO VALUE";
                _ValueTextBox.Text = "NO VALUE";
            }

            if (_sensor.Changeable == Changable.True)
            {
                _changeValue.Visibility=Visibility.Visible;
            }
        }

        //todo: when pressing back after 2 times, it goes back to connect page...:)
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
                e.Handled = true;
            }
        }

        private async void _deleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await _apiHandler.DeleteSensor(_sensor);
            if (result.Answer != null)
            {
                MessageDialog msgDialog = new MessageDialog("Sensor was deleted!");
                msgDialog.Commands.Add(new UICommand("OK"));
                var msgResult= await msgDialog.ShowAsync();
                if (msgResult.Label == "OK")
                {
                    Frame.GoBack();
                }
            }
        }

        private void _changeValue_OnClick(object sender, RoutedEventArgs e)
        {
            PageParameterPayload payload = new PageParameterPayload()
            {
                ApiHandlerParam = _apiHandler,
                SensorParam = _sensor
            };
            Frame.Navigate(typeof(ChangeSensorValuePage), payload);

        }
    }
}
