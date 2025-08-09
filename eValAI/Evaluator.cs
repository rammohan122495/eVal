namespace eValAI
{
    public class Evaluator
    {
        public static async Task<string> EvaluateAnswer(string question, string modelAnswer, string candidateAnswer, string apiKey)
        {  
            if (string.IsNullOrEmpty(apiKey))
            {
                return "Error: GEMINI_API_KEY not found in .env file or " +
                       "environment variables.";
            }

            var geminiClient = new GeminiApiClient(apiKey);

            string prompt =
                $"You are an AI assistant designed to evaluate answers." +
                $"{Environment.NewLine}Question: {question}" +
                $"{Environment.NewLine}Model Answer: {modelAnswer}" +
                $"{Environment.NewLine}Candidate Answer: {candidateAnswer}" +
                $"{Environment.NewLine}{Environment.NewLine}Evaluate the " +
                $"candidate answer based on the model answer and the " +
                $"question. Provide a score from 0 to 10, and a brief " +
                $"explanation." +
                $"{Environment.NewLine}Score:" +
                $"{Environment.NewLine}Explanation:";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            return await geminiClient.GenerateContentAsync(
                "gemini-2.5-flash", requestBody);
        }
    }
}
