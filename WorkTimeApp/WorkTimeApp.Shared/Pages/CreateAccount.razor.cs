using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkTimeApp.Shared.Model;

namespace WorkTimeApp.Shared.Pages
{
    public partial class CreateAccount
    {
        static HttpClient client = new ();
        private readonly PasswordHasher<UserModel> passwordHasher = new();

        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string? ConfirmPassword { get; set; }
        public string? ErrorMessage { get; set; }

        private async void Submit()
        {

            HttpStatusCode? request=null;
            if (Email == null)
            {
                ErrorMessage = "No email";
            }
            if (ConfirmPassword != Password)
            {
                ErrorMessage = "Password is not valid";

            }

#if ANDROID
            client.BaseAddress = new Uri("http://10.0.0.2:5076/");
#else
            client.BaseAddress = new Uri("http://localhost:5076/");
#endif
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

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
                // var response = await httpClient.GetAsync("http://10.0.2.2:5076/api/tasks");
#if ANDROID
                var response = await httpClient.PostAsJsonAsync("http://10.0.2.2:5076/api/users",userModel);
#else
                var response = await httpClient.PostAsJsonAsync("http://locallhost:5076/api/users",userModel);
#endif
                request = response.StatusCode;

                // var data = await response.Content.ReadAsStringAsync();
                var data = await response.Content.ReadAsStringAsync();

                // var url = await CreateProductAsync(userModel);
                //   Console.WriteLine($"Created at {url}");


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            if (request == HttpStatusCode.NotFound)
            {
                ErrorMessage = "The user with this email already exists";

            }
            if (request == HttpStatusCode.Created)
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