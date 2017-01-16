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
    public sealed partial class ReportingPage : Page
    {
        private ApiHandler _apiHandler;
        private List<Sensor> _sensorList;
        public ReportingPage()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _apiHandler = e.Parameter as ApiHandler;
        }

        private async void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            int countOfChangeAble = 0;
            int countOfNONChangeAble = 0;

            _sensorList = await _apiHandler.GetSensorList();
            foreach (var sensor in _sensorList)
            {
                if (sensor.Changeable == Changable.True)
                {
                    Thickness margin = changeAbleChart.Margin;
                    margin.Top -= 50;
                    changeAbleChart.Margin = margin;
                    changeAbleChart.Height += 50;

                    Thickness tbMargin = TB_Changeable.Margin;
                    tbMargin.Top -= 50;
                    TB_Changeable.Margin = tbMargin;

                    countOfChangeAble++;

                }
                else
                {
                    Thickness margin = NotChangableChart.Margin;

                    margin.Top -= 50;
                    NotChangableChart.Margin = margin;
                    NotChangableChart.Height += 50;

                    Thickness tbMargin = TB_NOTChangeable.Margin;
                    tbMargin.Top -= 50;
                    TB_NOTChangeable.Margin = tbMargin;
                    countOfNONChangeAble++;
                }
            }

            TB_Changeable.Text = countOfChangeAble.ToString();
            TB_NOTChangeable.Text = countOfNONChangeAble.ToString();


        }
    }
}
