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
            HttpStatusCode? request = null;
            if (Email == null)
            {
                ErrorMessage = "No email";
            }
            if (ConfirmPassword != Password)
            {
                ErrorMessage = "Password is not valid";
            }

            try
            {
                // Create a new user
                UserModel userModel = new UserModel()
                {
                    Email = Email,
                    Login = Email
                };
                userModel.Password = passwordHasher.HashPassword(userModel, Password);
                var httpClient = new HttpClient();

                // Use the injected ApiService instance
                var result = await ApiService.CreateAccountAsync("/api/users", userModel);
             
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (request == HttpStatusCode.NotFound)
            {
                ErrorMessage = "The user with this email already exists";
            }
            else if (request == HttpStatusCode.Created)
            {
                ErrorMessage = "User created";
            }
            else
            {
                ErrorMessage = "Unknown error";
            }
        }
    }
}