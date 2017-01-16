using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ChangeSensorValuePage : Page
    {
        private Sensor _sensor;
        private ApiHandler _apiHandler;

        public ChangeSensorValuePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PageParameterPayload pageParameterPayload =
            e.Parameter as PageParameterPayload;

            _apiHandler = pageParameterPayload.ApiHandlerParam;
            _sensor = pageParameterPayload.SensorParam;
        }

        private async void ChangeIt_ButtonOnClick(object sender, RoutedEventArgs e)
        {
            SIUnits siUnit = (SIUnits)Enum.Parse(typeof(SIUnits), SiUnitComboBox.SelectedItem.ToString());

            SensorData sensorData = new SensorData("_time_", ValueTextBox.Text, siUnit.ToString(), _sensor.Id);
            var result = await _apiHandler.ChangeSensorValue(_sensor.Id, sensorData);

            MessageDialog msgDialog = new MessageDialog("Sensor was changed!");
            msgDialog.Commands.Add(new UICommand("OK"));
            if (result.Answer != null)
            {
                var msgDialogResult = await msgDialog.ShowAsync();
                if (msgDialogResult.Label == "OK")
                {
                    Frame.GoBack();
                }
            }
        }

        private void Page_OnLoad(object sender, RoutedEventArgs e)
        {
            SensorName.Text = _sensor.Type;
            SiUnitComboBox.ItemsSource = Enum.GetValues(typeof(SIUnits));
            changeButton.IsEnabled = false;
        }

        private void ValueTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            changeButton.IsEnabled = true;
        }
    }
}
