using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WorkTime.Abstractions;
using WorkTimeApp.Shared.Model;
using WorkTimeApp.Shared.Services;

namespace WorkTimeApp.Shared.Services;
public class ApiService
{
    private readonly HttpClient _httpClient;
    private AuthenticationHeaderValue _token;  // Tu przechowujemy token
    public int _id; // Tu przechowujemy ID u¿ytkownika
    IPlatformInfo? PlatformInfo; // Inicjalizacja interfejsu IPlatformInfo
    public string BaseUrl { get; set; } = "http://localhost:5000"; // Domyœlny URL, mo¿na zmieniæ w konstruktorze
    public ApiService(HttpClient httpClient, IPlatformInfo? platformInfo)
    {
        _httpClient = httpClient;
        PlatformInfo = platformInfo;

        // Ensure PlatformInfo is not null before calling GetBaseUrl
        BaseUrl = PlatformInfo?.GetBaseUrl() ?? "http://localhost:5000";
    }

    public async Task<bool> LoginAsync(UserModel user)
    {
        UserModel loginData = user;
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(BaseUrl + "/login", content);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (obj != null && !string.IsNullOrEmpty(obj.Token))
            {

                _token = AuthenticationHeaderValue.Parse(obj.Token);
                _id = obj.Id;

                return true;
            }
        }
        return false;
    }
    public async Task<string> CreateAccountAsync(string page, UserModel user)
    {
        // Correctly create an AuthenticationHeaderValue instance

        var response = await _httpClient.PostAsJsonAsync(BaseUrl + page,user);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetProtectedDataAsync(string page)
    {
        if (string.IsNullOrEmpty(_token.ToString()))
            throw new InvalidOperationException("Brak tokena, zaloguj siê najpierw.");

        // Correctly create an AuthenticationHeaderValue instance
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.ToString());

        var response = await _httpClient.GetStringAsync(BaseUrl + page + _id);

        return response;
    }

    public async Task<string> GetProtectedDataWithoutUserIdAsync(string page)
    {
        if (string.IsNullOrEmpty(_token.ToString()))
            throw new InvalidOperationException("Brak tokena, zaloguj siê najpierw.");

        // Correctly create an AuthenticationHeaderValue instance
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.ToString());

        var response = await _httpClient.GetStringAsync(BaseUrl + page);

        return response;
    }
    public async Task<string> PostProtectedDataAsync<T>(string page, T PostObject)
    {
        if (string.IsNullOrEmpty(_token.ToString()))
            throw new InvalidOperationException("Brak tokena, zaloguj siê najpierw.");

        // Correctly create an AuthenticationHeaderValue instance
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.ToString());

        var response = await _httpClient.PostAsJsonAsync(BaseUrl + page, PostObject);

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> PutProtectedDataAsync<T>(string page, T PostObject)
    {
        if (string.IsNullOrEmpty(_token.ToString()))
            throw new InvalidOperationException("Brak tokena, zaloguj siê najpierw.");

        // Correctly create an AuthenticationHeaderValue instance
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.ToString());

        var response = await _httpClient.PutAsJsonAsync<T>(BaseUrl + page, PostObject);

        return await response.Content.ReadAsStringAsync();
    }

    public class LoginResponse
    {
        public string? Token { get; set; }
        public int Id { get; set; }

    }
}