using BlazorUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorUI.Services
{
    public class MusicService
    {
        private readonly HttpClient _httpClient;

        public MusicService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<IEnumerable<MusicViewModel>> GetAllMusicAsync()
        {
            var response = await _httpClient.GetAsync("music");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<MusicViewModel>>(responseContent);
        }

        public async Task<IEnumerable<MusicViewModel>> GetAllMusicByAlbumAsync(int AlbumId)
        {
            var response = await _httpClient.GetAsync($"music/album/{AlbumId}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<MusicViewModel>>(responseContent);
        }

        public async Task<MusicViewModel> GetMusicForViewAsync(int id)
        {
            var response = await _httpClient.GetAsync($"music/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<MusicViewModel>(responseContent);
        }

        //Task<MusicCUDTO> GetMusicForUpdateAsync(int Id);

        //Task<MusicCUDTO> GetMusicForUpdateAsync(string Name, string Author, int? Year);

        //Task<IEnumerable<MusicViewDTO>> GetMusicByNameAsync(string Name);

        //Task AddMusicAsync(MusicCUDTO musicCreateDTO);

        //Task UpdateMusicAsync(int Id, MusicCUDTO musicUpdateDTO);

        //Task DeleteMusicAsync(MusicViewDTO musicDTO);
    }
}
