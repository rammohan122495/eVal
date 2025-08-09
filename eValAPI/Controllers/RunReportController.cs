using eValService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eValAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunReportController : ControllerBase
    {
        private IRunReportService _runReportService { get; set; }
        public RunReportController(IRunReportService runReportService)
        {
                this._runReportService = runReportService;
        }

        [HttpPost("RunReport")]
        public async Task<IActionResult> PostRunReport() 
        {
            await Task.Delay(5000);
            try
            {
                return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
               

        [HttpGet("GetReportDetails")]
        public async Task<IActionResult> GetReportDetails()
        {
            try
            {
                var response = await this._runReportService.GetReportDetails();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
