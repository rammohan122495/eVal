using eValDTO.DTOs;
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
    }
}
