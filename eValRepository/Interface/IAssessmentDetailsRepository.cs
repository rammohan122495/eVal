using eValDTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValRepository.Interface
{
    public interface IAssessmentDetailsRepository
    {
        Task<List<AssessmentResultAudit>> GetAllAssessmentResultAudits(string attemptId);

        Task<bool> UpdateAssessmentResultAudit(int id, string actionStatus, string updateComment);

        Task<int> InsertAssessmentResultAudit(AssessmentResultAudit model);

        Task<bool> UpdateIAResponseScoreAndComment(int id, string score, string comment);
    }
}
