using App_Dating.Controllers;
using App_Dating.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace DatingApp_API.Test
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<UserController>>
    {
        readonly HttpClient _client;

        public UserControllerTest(WebApplicationFactory<UserController> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task GetUser_Id2_Status200()
        {
            var response = await _client.GetAsync("/User/2");

            var expected = HttpStatusCode.OK;
            var actual = response.StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetUsers_WithOutParameters_Status200()
        {
            //Arrange
            var response = await _client.GetAsync("/User");

            //Act
            var responseString = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(responseString);

            // Assert
            var expected = HttpStatusCode.OK;
            var actual = response.StatusCode;
            Assert.Equal(expected, actual);

            Assert.True(users.Any());
        }

        [Fact]
        public async Task PostUser_CorrectData_Success()
        {
            //Arrange
            var data = new
            {
                UserName = "BebitoFiuFiu",
                Name = "Martin",
                LastName = "Vizcarra",
                Age = "61",
                Gender = "M",
                City = "Moquegua",
                Email = string.Concat(Guid.NewGuid(), "@latinchat.com")
            };
            var payload = JsonConvert.SerializeObject(data);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PostAsync("/User",content);


            //Assert 
            response.EnsureSuccessStatusCode();
            var expected = "User created successfully.";
            var actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PostUser_EmailDuplicated_Status200()
        {
            //Arrange
            var data = new
            {
                UserName = "BebitoFiuFiu",
                Name = "Martin",
                LastName = "Vizcarra",
                Age = "61",
                Gender = "M",
                City = "Moquegua",
                Email = "BebitoFiuFiu@latinchat.com"
            };
            var payload = JsonConvert.SerializeObject(data);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PostAsync("/User", content);

            //Assert
            var expected = "The email entered is already registered.";
            var actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PostUser_UnderAge_Status400()
        {
            //Arrange
            var data = new
            {
                UserName = "AmorEnLlamas",
                Name = "Mariano",
                LastName = "Gonzales",
                Age = "17",
                Gender = "M",
                City = "Lima",
                Email = "MinistroDelAmor2@latinchat.com"
            };
            var payload = JsonConvert.SerializeObject(data);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PostAsync("/User", content);

            //Assert
            var expected = "The user must be of legal age.";
            var actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PostUser_EmailBadFormat_Status400()
        {
            //Arrange
            var data = new
            {
                UserName = "AmorEnLlamas",
                Name = "Julio",
                LastName = "Guzman",
                Age = "19",
                Gender = "Male",
                City = "Lima",
                Email = "AmorEnLlamas latinchat."
            };
            var payload = JsonConvert.SerializeObject(data);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PostAsync("/User", content);

            //Assert
            var expected = "The field Gender must be a string or array type with a maximum length of '1'.";
            var actual = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expected, actual.Errors.First().Value.First());
        }

        [Fact]
        public async Task PutUser_DataCorrect_Status200()
        {
            //Arrange
            var data = new
            {
                UserName = "BebitoFiuFiu",
                Name = "Martin",
                LastName = "Vizcarra",
                Age = "61",
                Gender = "M",
                City = "Lima",
                Email = "BebitoFiuFiu@latinchat.com"
            };
            var payload = JsonConvert.SerializeObject(data);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PutAsync("/User", content);

            //Assert
            response.EnsureSuccessStatusCode();
            var expected = "User updated successfully";
            var actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }
    }
}