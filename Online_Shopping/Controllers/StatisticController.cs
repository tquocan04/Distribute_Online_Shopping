using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    [Authorize(Roles = "Admin, Staff")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService) 
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRevenueInYearMonth(int year, string month)
        {
            Dictionary<string, decimal?> revenue = new Dictionary<string, decimal?>();

            if (month == "All")
                revenue = await _statisticService.GetRevenueCategoriesInYear(year);
            else
                revenue = await _statisticService.GetRevenueCategoriesInYearMonth(year, Int32.Parse(month));

            return Ok(revenue);
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetRevenueByCategoryInYearMonth(Guid categoryId, int year, string month)
        {
            Dictionary<string, decimal?> revenue = new Dictionary<string, decimal?>();

            if (month == "All")
                revenue = await _statisticService.GetRevenueByCategoriesInYear(categoryId, year);
            else
                revenue = await _statisticService.GetRevenueByCategoriesInYearMonth(categoryId, year, Int32.Parse(month));

            return Ok(revenue);
        }
    }
}
