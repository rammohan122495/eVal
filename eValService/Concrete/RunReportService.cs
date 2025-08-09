using eValDTO.DTOs;
using eValRepository.Interface;
using eValService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValService.Concrete
{
    public class RunReportService : IRunReportService
    {
        private IRunReportRepository _runReportRepository { get; set; }
        public RunReportService(IRunReportRepository runReportRepository)
        {
            this._runReportRepository = runReportRepository;
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
    }
}
