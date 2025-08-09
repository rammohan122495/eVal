using eValDTO.DTOs;
using eValService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eValAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiateAIController : ControllerBase
    {
        private IInitiateAIService _initiateAIService { get; set; }
        public InitiateAIController(IInitiateAIService initiateAIService)
        {
            this._initiateAIService = initiateAIService;
        }

        [HttpPost("InitiateAI")]
        public async Task<IActionResult> InitiateAI([FromBody] InitiateAIRequest objInitiateAIRequest)
        {
            try
            {
                var response = await this._initiateAIService.InitiateAI(objInitiateAIRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
