using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace implCommerce.Services  // Make sure to replace with your actual namespace
{
    public class OpenAIService
    {
        private readonly string _apiKey;
        private readonly string _endpoint;

        // Constructor that receives API Key and Endpoint from environment variables or configuration
        public OpenAIService(string apiKey, string endpoint)
        {
            _apiKey = apiKey;
            _endpoint = endpoint;
        }

        // Example method to interact with Azure OpenAI API for code generation, completions, etc.
        public async Task<string> GetAIResponseAsync(string prompt)
        {
            using (var client = new HttpClient())
            {
                // Set API endpoint and build the request body
                var requestBody = new
                {
                    prompt = prompt,
                    max_tokens = 100,
                    temperature = 0.7
                };

                var jsonRequest = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Add the API key to the headers
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);
                
                // Send the request to the Azure OpenAI API
                var response = await client.PostAsync(_endpoint, content);

                // Handle the response and extract the result
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    return result?.choices[0]?.text?.ToString();  // Adjust this part based on response structure
                }
                else
                {
                    throw new Exception($"Error calling OpenAI API: {response.ReasonPhrase}");
                }
            }
        }
    }
}
