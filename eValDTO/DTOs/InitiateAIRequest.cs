using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValDTO.DTOs
{
    public class InitiateAIRequest
    {

        public  int Id { get; set; }
        public int AssessmentId { get; set; }
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string ModelAnswer { get; set; }
    }
}
