using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EasyHome
{
    public sealed partial class MySensors : Page
    {
        private ApiHandler _apiHandler;
        private List<Sensor> _sensorList;

        private ObservableCollection<Sensor> _observableCollection;

        public MySensors()
        {
            this.InitializeComponent();
            _observableCollection = new ObservableCollection<Sensor>();
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _apiHandler= e.Parameter as ApiHandler;
            var returnedValue = await _apiHandler.GetSensorList();
            _sensorList = returnedValue;
            FillObserevableCollection(_sensorList);
            _listbox.DataContext = _observableCollection;
        }

        private void FillObserevableCollection(List<Sensor> sensors)
        {
            foreach (var sensor in sensors)
            {
                _observableCollection.Add(sensor);
            }
        }

        private void SensorClick(object sender, ItemClickEventArgs e)
        {
            Sensor sensor = e.ClickedItem as Sensor;
            PageParameterPayload payload = new PageParameterPayload()
            {
                ApiHandlerParam = _apiHandler,
                SensorParam = sensor
            };
            Frame.Navigate(typeof(SensorDataPage),payload);
        }

        private void CreateSensorOnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateSensorPage),_apiHandler);
        }
    }
}
