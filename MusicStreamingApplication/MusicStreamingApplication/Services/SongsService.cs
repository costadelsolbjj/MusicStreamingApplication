using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MusicStreamingApplication.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MusicStreamingApplication.Services
{
    public class SongsService
    {
        readonly HttpClient _client;
        public SongsService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        private const string SongsApi = "api/Songs";
        public async Task<List<Songs>> GetAllSongs(int pageNumber, int pageSize)
        {
            var json = await _client.GetStringAsync($"{SongsApi}?pageNumber={pageNumber}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<List<Songs>>(json);
        }

        public async Task<List<Songs>> SearchSongs(string searchSongQuery)
        {
            var json = await _client.GetStringAsync($"{SongsApi}/SearchSongs/" + searchSongQuery);
            return JsonConvert.DeserializeObject<List<Songs>>(json);
        }
    }
}
