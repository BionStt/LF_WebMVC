using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebMVC.Extensions;
using WebMVC.Models;

namespace WebMVC.HttpClients
{
    public class CatalogAPIHttpClient
    {
        private HttpClient _client { get; }

        public CatalogAPIHttpClient(HttpClient client)
        {
            _client = client;
            _client.Timeout = new TimeSpan(0, 0, 5);
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<CatalogItem> GetCatalogItemByIdAsync(int id, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/catalog/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                response.EnsureSuccessStatusCode();

                return stream.ReadAndDeserializeFromJson<CatalogItem>();
            }
        }

        public async Task<List<CatalogItem>> GetAllCatalogItemsAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/catalog");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                response.EnsureSuccessStatusCode();

                return stream.ReadAndDeserializeFromJson<List<CatalogItem>>();
            }
        }

    }
}