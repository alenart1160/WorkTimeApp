using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkTimeApp.Shared.Model;
using WorkTimeApp.Shared.Services;

namespace WorkTimeApp.Shared.Pages
{
    public partial class CreateAccount
    {
        static HttpClient client = new();
        private readonly PasswordHasher<UserModel> passwordHasher = new();

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!; // Inject NavigationManager

        [Inject]
        private ApiService ApiService { get; set; } = null!; // Inject ApiService

        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string? ConfirmPassword { get; set; }
        [Parameter]
        public string? ErrorMessage { get; set; }
        private void NavigateToHome()
        {
            NavigationManager.NavigateTo("");
        }
        private async Task Submit()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Podaj email.";
                return;
            }
            if (ConfirmPassword != Password)
            {
                ErrorMessage = "Has³a nie s¹ zgodne.";
                return;
            }

            try
            {
                UserModel userModel = new UserModel()
                {
                    Email = Email,
                    Login = Email
                };
                userModel.Password = passwordHasher.HashPassword(userModel, Password);

                HttpResponseMessage response = await ApiService.CreateAccountAsync("/api/users", userModel);

                if (response.IsSuccessStatusCode)
                {
                    ErrorMessage = "U¿ytkownik zosta³ utworzony.";
                }
                else if (response.StatusCode == HttpStatusCode.Conflict || response.StatusCode == HttpStatusCode.NotFound)
                {
                    ErrorMessage = "U¿ytkownik z tym adresem email ju¿ istnieje.";
                }
                else
                {
                    // Mo¿esz odczytaæ treœæ odpowiedzi, jeœli chcesz wyœwietliæ szczegó³y b³êdu
                    var content = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"B³¹d podczas rejestracji: {content}";
                }
            }
            catch (Exception e)
            {
                ErrorMessage = "B³¹d: " + e.Message;
            }
        }
    }
}