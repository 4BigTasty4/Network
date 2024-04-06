using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using static HM2.populationResponse;

namespace HM2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void searchBoxs(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Search")
            {
                textBox.Text = "";
            }
        }

        private async void searchBut(object sender, RoutedEventArgs e)
        {
            await Weather();
            await Attractions();
            await Population();
        }

        private async Task Weather()
        {
            string city = searchCn.Text;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/landon/EN"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "f1c09cc43fmsh69554c799d5db05p117206jsncd0cfd8f5c7a" },
                        { "X-RapidAPI-Host", "open-weather13.p.rapidapi.com" },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var weatherResponse = JsonConvert.DeserializeObject<weatherResponse.Rootobject>(responseBody);

                        searchResult.Text = $"{(weatherResponse.main.temp - 32) * 5 / 9} °C, ветер {weatherResponse.wind.speed} м/с, {weatherResponse.weather[0].description}";

                    }
                }
            }
        }
        private async Task Attractions()
        {
            string city = searchCn.Text;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://airbnb45.p.rapidapi.com/api/v1/searchLocation?query={city}"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "f1c09cc43fmsh69554c799d5db05p117206jsncd0cfd8f5c7a" },
                        { "X-RapidAPI-Host", "airbnb45.p.rapidapi.com" },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var attractionsResponse = JsonConvert.DeserializeObject<attractionsResponse.Rootobject>(responseBody);

                        searchResult1.Text = $"{attractionsResponse.data[2].explore_search_params.query}";
                    }
                }
            }
        }
        private async Task Population()
        {
            string city = searchCn.Text;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://city-by-api-ninjas.p.rapidapi.com/v1/city?name={city}&limit=1"),
                Headers =
            {
                { "X-RapidAPI-Key", "f1c09cc43fmsh69554c799d5db05p117206jsncd0cfd8f5c7a" },
                { "X-RapidAPI-Host", "city-by-api-ninjas.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var populationResponse = JsonConvert.DeserializeObject<List<City>>(responseBody);

                searchResult2.Text = $"Страна: {populationResponse.First().country} Столица: {populationResponse.First().is_capital} Население: {populationResponse.First().population:N0}";
            }
        }
    }
}
