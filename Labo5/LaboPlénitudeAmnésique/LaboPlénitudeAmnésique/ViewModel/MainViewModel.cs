using GalaSoft.MvvmLight;
using LaboPlénitudeAmnésique.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboPlénitudeAmnésique.ViewModel
{
    public class MainViewModel : ViewModelBase
   {
        private ObservableCollection<WeatherForecast> _forecast = null;

        public ObservableCollection<WeatherForecast> Forecast
        {
            get
            {
                return _forecast;
            }
            set
            {
                if(_forecast == value)
                {
                    return;
                }
                _forecast = value;
                RaisePropertyChanged("Forecast");
            }
        }

        //public bool IsInDesignMode { get; private set; }

        public MainViewModel()
        {
            if(IsInDesignMode)
            {
                var forecast = new Forecast() { CityName = "Namur" };
                var weatherForecasts = new List<WeatherForecast>();
                for(int i = 0; i < 40; i++)
                {
                    weatherForecasts.Add(new WeatherForecast()
                    {
                        Date = DateTime.Now.AddDays(i),
                        MaxTemp = 5 + i / 2,
                        MinTemp = 0 + i / 2,
                        WeatherDescription = "Peu de nuages",
                        WindSpeed = i % 7
                    });
                }
                forecast.WeatherForecasts = weatherForecasts;
                forecast = new ObservableCollection<WeatherForecast>(weatherForecasts);
            }
            else
            {
                InitializeAsync();
            }

        }
        public async Task InitializeAsync()
        {
            var service = new WeatherService();
            var forecast = await service.GetForecast();
            Forecast = new ObservableCollection<WeatherForecast>(forecast);
        }
   }
}
