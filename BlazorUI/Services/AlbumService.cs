using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorUI.Models;
using BlazorUI.Interfaces.IServices;

namespace BlazorUI.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly HttpClient _httpClient;

        public AlbumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AlbumViewModel>> GetAlbumsAsync()
        {
            var response = await _httpClient.GetAsync("album");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<AlbumViewModel>>(responseContent);
        }

        public async Task<AlbumViewModel> GetAlbumById(int AlbumId)
        {
            var response = await _httpClient.GetAsync($"album/{AlbumId}");
            response.EnsureSuccessStatusCode();
            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<AlbumViewModel>(responseContent);
        }
    }
}
