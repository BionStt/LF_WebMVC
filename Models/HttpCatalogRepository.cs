using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebMVC.Extensions;
using WebMVC.HttpClients;

namespace WebMVC.Models
{
    public class HttpCatalogRepository : ICatalogRepository
    {
        private readonly CatalogAPIHttpClient _catalogClient; 
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public HttpCatalogRepository(CatalogAPIHttpClient catalogAPIClient)
        {
            _catalogClient = catalogAPIClient;

            Console.WriteLine("----> HttpCatalogRepository Hit");
        }


        public List<CatalogItem> GetAllCatalogItems()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<CatalogItem>> GetAllCatalogItemsAsync()
        {
            Console.WriteLine("----> GetAllCatalogItemsAsync...");

            return await _catalogClient.GetAllCatalogItemsAsync(_cancellationTokenSource.Token);
        }

        
        public async Task<CatalogItem> GetCatalogItemByIdAsync(int id)
        {
            Console.WriteLine("----> GetAllCatalogItemByIdAsync...");

            return await _catalogClient.GetCatalogItemByIdAsync(id, _cancellationTokenSource.Token);
        }

        /*
        public async Task<List<CatalogItem>> GetAllCatalogItemsOldAsync()
        {
            Console.WriteLine("----> Getting Catalog Items...");
            //var response = await _httpClient.GetAsync("api/catalog");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/catalog");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            //Need to replace with Stream Reading?
            var content = await response.Content.ReadAsStringAsync();
            var cat = JsonConvert.DeserializeObject<List<CatalogItem>>(content);


            //_cancellationTokenSource.Cancel();
            //await GetCatalogItemWithTypedFactory(2, _cancellationTokenSource.Token);

            return cat;
        }

        //Non Interface Methods
        
        private async Task GetCatalogItemWithTypedFactory(int id, CancellationToken cancelationToken)
        {
            //var httpClient = _httpClientFactory.CreateClient("Catalog.API");

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/catalog/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                using (var response = await _catalogClient.Client.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead, cancelationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();

                    var cat = stream.ReadAndDeserializeFromJson<CatalogItem>();
                    Console.WriteLine($"---> id: {cat.Id} name: {cat.Name}");

                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"The operation was cancelled with message {ocException.Message}");
            }
        }
        */
        /*
        private async Task GetCatalogItemWithNamedFactory(int id, CancellationToken cancelationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("Catalog.API");

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/catalog/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                using (var response = await httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead, cancelationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();

                    var cat = stream.ReadAndDeserializeFromJson<CatalogItem>();
                    Console.WriteLine($"---> id: {cat.Id} name: {cat.Name}");

                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"The operation was cancelled with message {ocException.Message}");
            }
        }

        private async Task GetCatalogItemWithFactory(int id, CancellationToken cancelationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/catalog/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                using (var response = await httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead, cancelationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();

                    var cat = stream.ReadAndDeserializeFromJson<CatalogItem>();
                    Console.WriteLine($"---> id: {cat.Id} name: {cat.Name}");

                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"The operation was cancelled with message {ocException.Message}");
            }

        }

        private async Task GetCatalogItem(int id, CancellationToken cancelationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/catalog/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                using (var response = await _httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead))//,
                    //cancelationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();

                    var cat = stream.ReadAndDeserializeFromJson<CatalogItem>();
                    Console.WriteLine($"---> id: {cat.Id} name: {cat.Name}");

                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"The operation was cancelled with message {ocException.Message}");
            }
            
        }

        private async Task CreateCatalogItemAsync()
        {
            var catToCreate = new CatalogItemForCreation()
            {
                Name = "Predator"
            };

            var serializedCatToCreate = JsonConvert.SerializeObject(catToCreate);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/catalog");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedCatToCreate);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var cat = JsonConvert.DeserializeObject<CatalogItem>(content);
        }

        private async Task CreateCatalogItemWithStreamAsync()
        {
            var catToCreate = new CatalogItemForCreation()
            {
                Name = "Equalizer"
            };

            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(catToCreate);

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(
                HttpMethod.Post, "api/catalog"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();
                        var stream = await response.Content.ReadAsStreamAsync();
                        var cat = stream.ReadAndDeserializeFromJson<CatalogItem>();
                    }

                }
            }



        }

        private async Task UpdateCatalogItem()
        {
            var catToUpdate = new CatalogItem()
            {
                Id = 7,
                Name = "Pulp Fiction"
            };

            var serializedCatToUpdate = JsonConvert.SerializeObject(catToUpdate);

            var request = new HttpRequestMessage(HttpMethod.Put, "api/catalog/7");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedCatToUpdate);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var cat = JsonConvert.DeserializeObject<CatalogItem>(content);

        }

        private async Task DeleteCatalogItem()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/catalog/7");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
        }

        */
    }
}