using eValDTO.DTOs;
using eValRepository.Interface;
using eValService.Interface;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Concrete
{
    public class AssessmentDetailsService : IAssessmentDetailsService
    {
        private IAssessmentDetailsRepository _assessmentDetailsRepository { get; set; }
        private readonly string _wsToken;
        private readonly string _apiToken;
        private readonly string _apiUrl;

        public AssessmentDetailsService(IAssessmentDetailsRepository assessmentDetailsRepository, IConfiguration configuration) 
        {
            this._assessmentDetailsRepository = assessmentDetailsRepository;
            _wsToken = configuration.GetConnectionString("WsToken");
            _apiToken = configuration.GetConnectionString("ApiToken");
            _apiUrl = configuration.GetConnectionString("BaseUrl");
        }

        public async Task<List<AssessmentResultAudit>> GetAllAssessmentResultAudits(string attemptId)
        {
            try
            {
                return await this._assessmentDetailsRepository.GetAllAssessmentResultAudits(attemptId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAssessmentResultAudit(int id, string actionStatus, string updateComment)
        {
            try
            {
                return await this._assessmentDetailsRepository.UpdateAssessmentResultAudit(id, actionStatus, updateComment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UpdateAxcelerate(ScoredItemRequest objScoredItemRequest)
        {
            try
            {
                string url = $"{_apiUrl}/api/v2/assessments/purple/responses/bulk";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("apitoken", _apiToken);
                    client.DefaultRequestHeaders.Add("wstoken", _wsToken);

                    // Serialize object to JSON
                    var jsonContent = JsonConvert.SerializeObject(objScoredItemRequest);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    try
                    {
                        HttpResponseMessage response = await client.PostAsync(url, httpContent);
                        response.EnsureSuccessStatusCode();

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("API Response:");

                        JObject root = JObject.Parse(apiResponse);
                        var results = root["CONTENT"]?["results"];

                        Console.WriteLine(results);
                        return apiResponse;
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        return ex.Message;
                    }
                }

            }
            catch
            {
                throw; // Keep original exception details
            }
        }

    }
}
