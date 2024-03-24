using HM3.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HM3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Search")
            {
                textBox.Text = "";
            }
        }

        private async void Click_Google(object sender, RoutedEventArgs e)
        {
            string userQuery = searchBox.Text;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://google-web-search1.p.rapidapi.com/?query={userQuery}&limit=20&related_keywords=true"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "f1c09cc43fmsh69554c799d5db05p117206jsncd0cfd8f5c7a" },
                        { "X-RapidAPI-Host", "google-web-search1.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        var searchResponse = JsonConvert.DeserializeObject<SearchResponse.Rootobject>(responseBody);

                        var firstResult = searchResponse?.results?.FirstOrDefault();

                        if (firstResult != null)
                        {
                            searchResults.Text = $"URL: {firstResult.url}{Environment.NewLine}Title: {firstResult.title}{Environment.NewLine}Description: {firstResult.description}";
                        }
                        else
                        {
                            searchResults.Text = "No search results found.";
                        }
                    }
                    else
                    {
                        searchResults.Text = $"Error: {response.StatusCode}";
                    }
                }
            }
        }

        private async void Click_Bing(object sender, RoutedEventArgs e)
        {
            string userQuery = searchBox.Text;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://bing-web-search4.p.rapidapi.com/bing-search"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "f1c09cc43fmsh69554c799d5db05p117206jsncd0cfd8f5c7a" },
                        { "X-RapidAPI-Host", "bing-web-search4.p.rapidapi.com" },
                    },
                    Content = new StringContent($"{{\"keyword\": \"{userQuery}\",\"page\": 1,\"lang\": \"en\",\"region\": \"us\"}}")
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                };

                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        var BingResponse = JsonConvert.DeserializeObject<BingResponse.Rootobject>(responseBody);

                        var firstResult = BingResponse?.search_results?.FirstOrDefault();

                        if (firstResult != null)
                        {
                            searchResults.Text = $"URL: {firstResult.url}{Environment.NewLine}Title: {firstResult.title}{Environment.NewLine}{firstResult.caption}";
                        }
                        else
                        {
                            searchResults.Text = "No search results found.";
                        }
                    }
                    else
                    {
                        searchResults.Text = $"Error: {response.StatusCode}";
                    }
                }
            }
        }
    }
}