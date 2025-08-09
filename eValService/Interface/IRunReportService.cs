using eValDTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Interface
{
    public interface IRunReportService
    {
        Task<bool> PostRunReport();
        Task<List<AssessmentDetails>> GetReportDetails();

        Task<List<AssessmentResultAudit>> GetQuestionbyAttemptId(string attemptId);
    }
}
