using eValDTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValRepository.Interface
{
    public interface IRunReportRepository
    {
        Task<List<AssessmentDetails>> GetReportDetails();
    }
}
