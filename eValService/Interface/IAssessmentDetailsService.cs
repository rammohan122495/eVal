using eValDTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Interface
{
    public interface IAssessmentDetailsService
    {
        Task<List<AssessmentResultAudit>> GetAllAssessmentResultAudits(string attemptId);

        Task<bool> UpdateAssessmentResultAudit(int id, string actionStatus, string updateComment);
        Task<string> UpdateAxcelerate(ScoredItemRequest objScoredItemRequest);
    }
}
