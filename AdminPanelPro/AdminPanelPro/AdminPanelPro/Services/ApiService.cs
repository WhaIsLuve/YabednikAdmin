
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AdminPanelPro.Models;

namespace AdminPanelPro.Services;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient http)
    {
        _http = http;

        var bytes = Encoding.UTF8.GetBytes("admin:admin");
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
    }

    public async Task<List<User>?> GetUsers() =>
        await _http.GetFromJsonAsync<List<User>>("/api/admin/users");

    public async Task CreateUser(User user) =>
        await _http.PostAsJsonAsync("/api/admin/users", user);

    public async Task UpdateUser(User user) =>
        await _http.PutAsJsonAsync("/api/admin/users", user);

    public async Task DeleteUser(string email)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, "/api/admin/users")
        {
            Content = JsonContent.Create(new { email })
        };
        await _http.SendAsync(req);
    }

    public async Task<List<Criteria>?> GetCriteria() =>
        await _http.GetFromJsonAsync<List<Criteria>>("/api/admin/criterias");

    public async Task CreateCriteria(object dto) =>
        await _http.PostAsJsonAsync("/api/admin/criterias", dto);

    public async Task UpdateCriteria(Guid id, object dto) =>
        await _http.PutAsJsonAsync($"/api/admin/criterias/{id}", dto);

    public async Task DeleteCriteria(Guid id) =>
        await _http.DeleteAsync($"/api/admin/criterias/{id}");

    public async Task<List<ScorableEntity>?> GetEntities() =>
        await _http.GetFromJsonAsync<List<ScorableEntity>>("/api/admin/scorableEntities");

    public async Task CreateEntity(object dto) =>
        await _http.PostAsJsonAsync("/api/admin/scorableEntities", dto);

    public async Task UpdateEntity(Guid id, object dto) =>
        await _http.PutAsJsonAsync($"/api/admin/scorableEntities/{id}", dto);

    public async Task DeleteEntity(Guid id) =>
        await _http.DeleteAsync($"/api/admin/scorableEntities/{id}");
}
