using eValDTO.DTOs;
using eValRepository.Interface;
using eValService.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace eValService.Concrete
{
    public class RunReportService : IRunReportService
    {
        private IRunReportRepository _runReportRepository { get; set; }
        private IAssessmentDetailsRepository _assessmentDetailsRepository { get; set; }

        private readonly string _wsToken;
        private readonly string _apiToken;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public RunReportService(IRunReportRepository runReportRepository, IAssessmentDetailsRepository assessmentDetailsRepository, IConfiguration configuration)
        {
            this._runReportRepository = runReportRepository;
            this._assessmentDetailsRepository = assessmentDetailsRepository;
            _wsToken = configuration.GetConnectionString("WsToken");
            _apiToken = configuration.GetConnectionString("ApiToken");
            _apiUrl = configuration.GetConnectionString("BaseUrl");
            _apiKey = configuration.GetConnectionString("GeminiApiKey");
        } 

        public Task<bool> PostRunReport()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AssessmentDetails>> GetReportDetails()
        {
            try
            {
                return await this._runReportRepository.GetReportDetails();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<AssessmentResultAudit>> GetQuestionbyAttemptId(string attemptId)
        {
            try
            {
                await this.ConsumeAxcelerateQuestionAPI(attemptId);
                return await this._assessmentDetailsRepository.GetAllAssessmentResultAudits(attemptId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ConsumeAxcelerateQuestionAPI(string attemptId)
        {
            string url = $"{_apiUrl}/api/v2/assessments/purple/attempts/{attemptId}/responses"; 
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("apitoken", _apiToken);
                client.DefaultRequestHeaders.Add("wstoken", _wsToken);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("API Response:");

                    JObject root = JObject.Parse(apiResponse);

                    var results = root["CONTENT"]?["results"];

                    if (results != null)
                    {
                        AssessmentResultAudit objAssessmentResultAudit = new AssessmentResultAudit();
                        foreach (var result in results)
                        {
                            string question = result["item"]?["description"]?.ToString();
                            string candidateAnswer = result["submission"]?.ToString();
                            string modelAnswer = result["item"]?["assessorText"]?.ToString();
                            int assessmentId = Convert.ToInt32(result["item"]?["assessmentItemID"]?.ToString());
                            int questionId = Convert.ToInt32(result["responseID"]?.ToString());
                            int studentId = Convert.ToInt32(result["respondee"]?["userID"]?.ToString());
                            string studentName = result["respondee"]?["username"]?.ToString();
                            objAssessmentResultAudit = new AssessmentResultAudit();
                            objAssessmentResultAudit.AssessmentId = assessmentId;
                            objAssessmentResultAudit.AttemptId = Convert.ToInt32(attemptId);
                            objAssessmentResultAudit.QuestionId = questionId;
                            objAssessmentResultAudit.Question = question;
                            objAssessmentResultAudit.Answer = candidateAnswer;
                            objAssessmentResultAudit.ModelAnswer = modelAnswer;
                            objAssessmentResultAudit.AIResponse = "N"; 
                            objAssessmentResultAudit.IsVerify = false;
                            objAssessmentResultAudit.ActionStatus = string.Empty;
                            objAssessmentResultAudit.PushedToAccelerate = false;
                            objAssessmentResultAudit.UpdateComment = "";
                            objAssessmentResultAudit.StudentId = studentId;
                            objAssessmentResultAudit.StudentName = studentName;
                            objAssessmentResultAudit.CreatedOn = DateTime.Now;
                            objAssessmentResultAudit.Score = 0;
                            objAssessmentResultAudit.Comment = "";
                            //string question = "<p>Explain in approximately 75 words, what is the <strong>purpose </strong>of a trust account in real estate?</p>";
                            //string modelAnswer = "<p><strong>Benchmark: </strong></p><p>Student responses will vary, however should encompass the below: </p><p><em>A trust account is very simply, an <strong>account that holds other people’s money</strong>. A real estate agency is required to use a designated bank account where <strong>funds related to real estate transactions are held by an approved financial institution</strong>. </em></p><p><em>Trust accounts are in <strong>place to ensure that funds are held securely and exclusively on behalf of someone</strong> in a real estate transaction.  </em></p><p><em>One of the most important functions of a real estate agency is to hold funds on behalf of others. In such circumstances, those funds are referred to as ‘trust funds’ – held ‘in trust’, by the agent, on behalf of either the rental provider or the vendor/buyer. Trust accounts play a crucial role in maintaining transparency, compliance with legislative requirements and ensuring the security of client funds in real estate transactions.</em></p><p><strong>Overall, the student should demonstrate knowledge of a Trust Account being a separate account being used to hold money that does not belong to the agency. </strong></p><p><span style=\"color: #ff7200;\"><strong>Ma</strong><strong>pping:</strong><br />PC1.2 Explain the purpose of trust accounts in real estate</span></p><p><span style=\"color: #ff7200;\">KE3 purpose of trust accounts in real estate</span></p><p><span style=\"color: #ff7200;\">PE1 explain the purpose of trust accounts in real estate</span></p><p> </p>";
                            //string candidateAnswer = "The purpose of a trust account in real estate is to securely hold money received on behalf of others, such as deposits, rental bonds, or rent payments. It ensures that client funds are kept separate from the agency’s operating money, protecting the client’s financial interests. Trust accounts are legally regulated to maintain transparency, prevent misuse of funds, and provide accountability. This system builds trust between agents, buyers, sellers, landlords, and tenants throughout the real estate transaction process.\n\n\n\n\n\n\n\n\n\n\nAsk ChatGPT";
                            //string apiResponsee = await eValAI.Evaluator.EvaluateAnswer(question, modelAnswer, candidateAnswer, _apiKey);

                            //var lines = apiResponsee.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                            //string scoreLine = lines.FirstOrDefault(line => line.StartsWith("Score:"));
                            //string explanationLine = string.Join(" ", lines.SkipWhile(line => !line.StartsWith("Explanation:")).ToList());

                            //int score = int.Parse(scoreLine.Replace("Score:", "").Split('/')[0].Trim());
                            //string explanation = explanationLine.Replace("Explanation:", "").Trim();
                            //objAssessmentResultAudit.Score = score;
                            //objAssessmentResultAudit.Comment = explanation;

                            this._assessmentDetailsRepository.InsertAssessmentResultAudit(objAssessmentResultAudit);
                            Console.WriteLine(apiResponse);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No results found.");
                    }

                    Console.WriteLine(results);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                }
            }
        }
    }
}
