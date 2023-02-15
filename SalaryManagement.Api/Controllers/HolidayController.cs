using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Infrastructure.Persistence;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;

        public HolidayController(IHolidayService holidayService, IMapper mapper)
        {
            _holidayService = holidayService;
            _mapper = mapper;
        }

        //private static List<Holiday> holidays = new List<Holiday>
        //{

        //    new Holiday{HolidayId = "hld_1",StartDate =  new DateTime(2023, 01, 07),EndDate =  new DateTime(2023, 01, 10),IsDelete = "1" },
        //     new Holiday{HolidayId = "hld_2",StartDate =  new DateTime(2023, 02, 07),EndDate =  new DateTime(2023, 02, 10),IsDelete = "0" }
        //};


        [HttpGet("get-all-holiday")]
        public async Task<IActionResult> GetAllHoliday()
        {
            var holidays = await _holidayService.GetAllHoliday();
            return Ok(holidays);
        }
    }
}
