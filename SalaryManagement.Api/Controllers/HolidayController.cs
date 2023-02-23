using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Infrastructure.Persistence;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/")]
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

        [HttpGet("holidays")]
        public async Task<IActionResult> GetAllHoliday()
        {
            var holidays = await _holidayService.GetAllHoliday();

            return Ok(holidays);
        }

        [HttpGet("holidays/{holidayId}")]
        public async Task<IActionResult> FindById(string id)
        {
            var holiday = await _holidayService.GetHolidaysById(id);

            if (holiday == null)

            {
                return NotFound("Holiday not found");
            }
            else
            {

                return Ok(holiday);
            }
        }



        [HttpPost("holidays")]
        public async Task<IActionResult> AddHoliday(HolidayRequest request)
        {
            await Task.CompletedTask;
            string id = Guid.NewGuid().ToString();

            Holiday holiday = new Holiday
            {
                HolidayId = id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                HolidayName = request.HolidayName,
                IsDeleted = true,
                IsPaid = true
            };
            var result = await _holidayService.AddHoliday(holiday);

            return Ok(result);
        }

        [HttpDelete("holidays/{holidayId}")]
        public async Task<IActionResult> DeleteHoliday(string holidayId)
        {
            string id = holidayId;
            Holiday holiday = new Holiday
            {
                HolidayId = id,
                IsDeleted = false
            };

            var result = await _holidayService.DeleteHoliday(holiday);
            var msg = "";

            if (result)

            {
                msg = "Update successfully";
            }
            else
            {
                return NotFound("Holiday not found");
            }
            return Ok(msg);
        }

        [HttpPut("holidays/{holidayId}")]
        public async Task<IActionResult> UpdateHoliday(string holidayId, HolidayUpdate request)
        {
            var existHoliday = await _holidayService.GetHolidaysById(holidayId);
            if (existHoliday == null)
            {
                return NotFound();
            }

            var holiday = existHoliday;

            if (request.StartDate.HasValue)
            {
                holiday.StartDate = request.StartDate.Value;
            }

            if (request.EndDate.HasValue)
            {
                holiday.EndDate = request.EndDate.Value;
            }

            if (!string.IsNullOrEmpty(request.HolidayName))
            {
                holiday.HolidayName = request.HolidayName;
            }

            if (!string.IsNullOrEmpty(request.IsPaid.ToString()))
            {
                holiday.IsPaid = request.IsPaid;
            }

            await _holidayService.UpdateHoliday(holiday);

            return Ok(holiday);
        }
    }
}

