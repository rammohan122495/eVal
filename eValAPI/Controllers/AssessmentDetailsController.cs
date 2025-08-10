using Azure.Core;
using eValDTO.DTOs;
using eValService.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eValAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentDetailsController : ControllerBase
    {
        private IAssessmentDetailsService _assessmentDetailsService {  get; set; }
        public AssessmentDetailsController(IAssessmentDetailsService assessmentDetailsService)
        {
                this._assessmentDetailsService = assessmentDetailsService;
        }

        [HttpGet("GetAllAssessmentResultAudits")]
        public async Task<IActionResult> GetAllAssessmentResultAudits(string attemptId)
        {
            try
            {
                var response = await this._assessmentDetailsService.GetAllAssessmentResultAudits(attemptId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UpdateAssessmentResultAudit")]
        public async Task<IActionResult> UpdateAssessmentResultAudit(string id, string actionStatus, string updateComment)
        {
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(actionStatus) && !string.IsNullOrEmpty(updateComment))
                {
                    var response = await this._assessmentDetailsService.UpdateAssessmentResultAudit(Convert.ToInt16(id), actionStatus, updateComment);
                    return Ok(response);
                }
                return NotFound(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateAxcelerate")]
        public async Task<IActionResult> UpdateAxcelerate(ScoredItemRequest objScoredItemRequest)
        {
            try
            {
                if (objScoredItemRequest == null || objScoredItemRequest.ScoredItemDTOs == null || !objScoredItemRequest.ScoredItemDTOs.Any())
                {
                    return BadRequest("Invalid or empty request.");
                }
                var response = await this._assessmentDetailsService.UpdateAxcelerate(objScoredItemRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
