using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuxularyApp.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://api.example.com/data");
                response.EnsureSuccessStatusCode(); // Выбросит исключение, если код состояния не успешный
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                // Обработка ошибок HTTP-запроса
                Console.WriteLine($"Произошла ошибка при выполнении запроса: {e.Message}");
                return null;
            }
        }
    }
}
