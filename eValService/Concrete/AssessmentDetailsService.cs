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
    public class AssessmentDetailsService : IAssessmentDetailsService
    {
        private IAssessmentDetailsRepository _assessmentDetailsRepository { get; set; }
        public AssessmentDetailsService(IAssessmentDetailsRepository assessmentDetailsRepository) 
        {
            this._assessmentDetailsRepository = assessmentDetailsRepository;
        }

        public async Task<List<AssessmentResultAudit>> GetAllAssessmentResultAudits(string attemptId)
        {
            try
            {
                return await this._assessmentDetailsRepository.GetAllAssessmentResultAudits(attemptId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAssessmentResultAudit(int id, string actionStatus, string updateComment)
        {
            try
            {
                return await this._assessmentDetailsRepository.UpdateAssessmentResultAudit(id, actionStatus, updateComment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
