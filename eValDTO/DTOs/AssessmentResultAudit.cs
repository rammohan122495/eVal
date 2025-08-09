using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValDTO.DTOs
{
    public class AssessmentResultAudit
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string ModelAnswer { get; set; }
        public string AIResponse { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }
        public bool IsVerify { get; set; }
        public string ActionStatus { get; set; }
        public bool PushedToAccelerate { get; set; }
        public string UpdateComment { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }

}
