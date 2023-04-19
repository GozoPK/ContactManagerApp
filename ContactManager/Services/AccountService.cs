using ContactManager.Extensions;
using ContactManager.Interfaces;
using ContactManager.Models;
using System.Text;
using System.Text.Json;

namespace ContactManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public AccountService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<User> Login(LoginModel model)
        {
            var url = _config["apiUrl"];

            var request = JsonSerializer.Serialize(model);
            var requestContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{url}/login", requestContent);

            return await response.ReadResponse<User>();
        }
    }
}
