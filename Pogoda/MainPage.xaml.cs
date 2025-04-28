using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace Pogoda
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadWeatherData();
        }

        private async void LoadWeatherData()
        {
            string url = "https://api.openweathermap.org/data/2.5/weather?lat=50.72&lon=23.22&appid=0c48b0e78aaf745e4cb29b309aa64923";
            string json;

            using (WebClient wc = new WebClient())
            {
                json = await wc.DownloadStringTaskAsync(url);
            }

            Weather w = JsonSerializer.Deserialize<Weather>(json);

            lblMiasto.Text = $"{w.name}";
            string s = $"temperatura: {(int)(w.main.temp - 273)} °C\n";
            s += $"ciśnienie: {w.main.pressure} hPa\n";
            s += $"wilgotność: {w.main.humidity} %\n";
            lblWynik.Text = s;

            string imgURL = "http://openweathermap.org/img/wn/" + w.weather[0].icon + ".png";
            imgPogoda.Source = imgURL;

            url = "https://api.openweathermap.org/data/2.5/forecast?lat=50.72&lon=23.22&appid=0c48b0e78aaf745e4cb29b309aa64923";
            using (WebClient wc = new WebClient())
            {
                json = await wc.DownloadStringTaskAsync(url);
            }

            Forecast f = JsonSerializer.Deserialize<Forecast>(json);

            lblMiasto.Text = $"{f.city.name}";


            for (int i = 0; i < 4; i++)
            {
                var forecast = f.list[i];
                var time = DateTime.Parse(forecast.dt_txt).ToString("HH:mm");
                var temp = (int)(forecast.main.temp - 273);
                var icon = forecast.weather[0].icon;

                switch (i)
                {
                    case 0:
                        lbl00.Text = time;
                        img00.Source = $"http://openweathermap.org/img/wn/{icon}.png";
                        lblTemp00.Text = $"{temp} °C";
                        break;
                    case 1:
                        lbl10.Text = time;
                        img10.Source = $"http://openweathermap.org/img/wn/{icon}.png";
                        lblTemp10.Text = $"{temp} °C";
                        break;
                    case 2:
                        lbl20.Text = time;
                        img20.Source = $"http://openweathermap.org/img/wn/{icon}.png";
                        lblTemp20.Text = $"{temp} °C";
                        break;
                    case 3:
                        lbl30.Text = time;
                        img30.Source = $"http://openweathermap.org/img/wn/{icon}.png";
                        lblTemp30.Text = $"{temp} °C";
                        break;
                }
            }
        }
    }

    public class Forecast
    {
        public string cod { get; set; }
        public City city { get; set; }
        public List<Prognoza> list { get; set; }
    }

    public class Prognoza
    {
        public int dt { get; set; }
        public string dt_txt { get; set; }
        public MainInfo main { get; set; }
        public List<WeatherInfo> weather { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class WeatherInfo
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Weather
    {
        public Coord coord { get; set; }
        public List<WeatherInfo> weather { get; set; }
        public string @base { get; set; }
        public MainInfo main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class MainInfo
    {
        public double temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
}