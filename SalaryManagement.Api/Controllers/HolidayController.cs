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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllHoliday()
        {
            var holidays = await _holidayService.GetAllHoliday();

            return Ok(holidays);
        }

        [HttpGet("{id}")]
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



        [HttpPost("")]
        public async Task<IActionResult> AddHoliday(HolidayRequest request)
        {
            await Task.CompletedTask;
            string id = Guid.NewGuid().ToString();

            Holiday holiday = new Holiday
            {
                HolidayId = id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                HolidayName= request.HolidayName,
                IsDeleted = true
            };
            var result = await _holidayService.AddHoliday(holiday);

            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteHoliday(HolidayDelete request)
        {
            Holiday holiday = new Holiday
            {
                HolidayId = request.Id,
                IsDeleted= false
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

        [HttpPut("update")]
        public async Task<IActionResult> UpdateHoliday(HolidayUpdate request)
        {
            Holiday holiday = new Holiday
            {
                HolidayId = request.Id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                HolidayName = request.HolidayName
            };

            var result = await _holidayService.UpdateHoliday(holiday);
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
    }
}

