using ContactManager.Extensions;
using ContactManager.Interfaces;
using ContactManager.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace ContactManager.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public ContactService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<Contact> CreateContact(Contact model, string token)
        {
            var url = _config["apiUrl"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = JsonSerializer.Serialize(model);
            var requestContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{url}/contacts", requestContent);

            return await response.ReadResponse<Contact>();
        }

        public async Task DeleteContact(int id, string token)
        {
            var url = _config["apiUrl"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await _client.DeleteAsync($"{url}/contacts/{id}");
        }

        public async Task<IEnumerable<Contact>> GetContacts(string token)
        {
            var url = _config["apiUrl"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{url}/contacts");

            return await response.ReadResponse<IEnumerable<Contact>>();
        }

        public async Task<Contact> GetContact(int id, string token)
        {
            var url = _config["apiUrl"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{url}/contacts/{id}");

            return await response.ReadResponse<Contact>();
        }

        public async Task UpdateContact(int id, UpdateContactModel model, string token)
        {
            var url = _config["apiUrl"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = JsonSerializer.Serialize(model);
            var requestContent = new StringContent(request, Encoding.UTF8, "application/json");

            await _client.PutAsync($"{url}/contacts/{id}", requestContent);
        }
    }
}
