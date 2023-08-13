using App;
using App.Api;
using App.Api.Controllers;
using App.Api.Controllers.V1;
using App.AplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ApiController]
    public class CalculatorController : Controller
    {
        private readonly IInvestmentCalculatorService _investmentCalculatorService;
        public CalculatorController(IInvestmentCalculatorService investmentCalculatorService)
        {
            _investmentCalculatorService = investmentCalculatorService;
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("cdb/{amount}/{months}")]
        public async Task<IActionResult> GetCdbSimulation([FromRoute] decimal amount, [FromRoute] int months)
        {
            var result = await _investmentCalculatorService.CalculateCdbSimulation(amount, months);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
