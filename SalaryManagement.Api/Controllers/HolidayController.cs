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
    [Route("api/v1/holidays")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;

        public HolidayController(IHolidayService holidayService, IMapper mapper)
        {
            _holidayService = holidayService;
            _mapper = mapper;
        }

        [HttpGet("getAllHolidays")]
        public async Task<IActionResult> GetAllHoliday()
        {
            var holidays = await _holidayService.GetAllHoliday();
            return Ok(holidays);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            var holiday = await _holidayService.GetHolidaysById(id);
            return Ok(holiday);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddHoliday(Holiday holiday)
        {
            var result = await _holidayService.AddHoliday(holiday);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(string id)
        {
            var result = await _holidayService.DeleteHoliday(id);
            if(result is null)
            {
                return NotFound("Holiday not found!!!");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHoliday(string id, Holiday request)
        {
            var result = await _holidayService.UpdateHoliday(id, request);
            if(result is null)
            {
                return NotFound("Holiday not found!!!");
            }
            return Ok(result);
        }
    }
}
