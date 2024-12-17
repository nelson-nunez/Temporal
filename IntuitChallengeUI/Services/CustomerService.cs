using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using System.Net.Http;
using Model;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace IntuitChallengeUI.Services
{
    public class CustomerService
    {
        public readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            this._httpClient = httpClient;            
            //Se define la url de origen de la API
            _httpClient.BaseAddress = new Uri("https://localhost:44387");
        }

        public async Task<List<Customer>> GetCustomerListAsync(DataManagerRequest dm)
        {
            string url = $"/Customer?name=todos";

            if (dm.Search != null && dm.Search.Any())
            {
                var searchKey = dm.Search.FirstOrDefault().Key.ToLower();
                url = $"/Customer?name={ searchKey}";
            }
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<List<Customer>>().Result;
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }   
        
        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"/Customer/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<Customer>().Result;
                return result;
            }
            else
            {
                throw new Exception("Error");
            }
        }   
        
        public async Task<long> CreateCustomerAsync(Customer customer)
        {
            var str = new StringContent(JsonSerializer.Serialize(customer), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Customer", str);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<long>();
            }
            else
            {
                var errorDto = await response.Content.ReadFromJsonAsync<Exception>();
                throw new Exception(errorDto.Message);
            }
        }   

        public async Task<long> UpdateCustomerAsync(Customer customer)
        {
            var str = new StringContent(JsonSerializer.Serialize(customer), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/Customer", str);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<long>();
            }
            else
            {
                var errorDto = await response.Content.ReadFromJsonAsync<Exception>();
                throw new Exception(errorDto.Message);
            }
        }   
        
        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            var response = await _httpClient.DeleteAsync($"/Customer/{customer.Id}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<bool>();
            else
            {
                var errorDto = await response.Content.ReadFromJsonAsync<Exception>();
                throw new Exception(errorDto.Message);
            }
        }   
    }
}

