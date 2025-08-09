using eValDTO.DTOs;
using eValRepository.Interface;
using eValService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Concrete
{
    public class InitiateAIService : IInitiateAIService
    {
        private IAssessmentDetailsRepository _assessmentDetailsRepository { get; set; }

        private readonly string _apiKey;
        public InitiateAIService(IConfiguration configuration, IAssessmentDetailsRepository assessmentDetailsRepository)
        {
            _apiKey = configuration.GetConnectionString("GeminiApiKey");
            this._assessmentDetailsRepository = assessmentDetailsRepository;
        }

        public async Task<string> InitiateAI(List<InitiateAIRequest> objInitiateAIRequest)
        {
            try
            {
                foreach (var request in objInitiateAIRequest)
                {

                    string apiResponsee = await eValAI.Evaluator.EvaluateAnswer(request.Question, request.ModelAnswer, request.ModelAnswer, _apiKey);

                    if (apiResponsee != null)
                    {
                        var lines = apiResponsee.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                        string scoreLine = lines.FirstOrDefault(line => line.StartsWith("Score:"));
                        string explanationLine = string.Join(" ", lines.SkipWhile(line => !line.StartsWith("Explanation:")).ToList());

                        int score = int.Parse(scoreLine.Replace("Score:", "").Split('/')[0].Trim());
                        string explanation = explanationLine.Replace("Explanation:", "").Trim();

                        this._assessmentDetailsRepository.UpdateIAResponseScoreAndComment(request.Id, score, explanation);
                    }
                }
                return "Processed sucessfully";
            }
            catch (Exception ex)
            {
                return $"Exception occured - {ex.Message}";
                throw;
            }
        }
    }
}
