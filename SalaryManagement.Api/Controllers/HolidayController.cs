using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Contracts;
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
            var response = new Response<object>();
            var holidays = await _holidayService.GetAllHoliday();
            if(holidays != null)
            {
                response.StatusCode = 200;
                response.Message = "Successfully";
                response.Data = holidays;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Failed";
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            var response = new Response<object>();
            var holiday = await _holidayService.GetHolidaysById(id);
            if (holiday != null)
            {
                response.StatusCode = 200;
                response.Message = "Successfully";
                response.Data = holiday;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Not found";
            }
            return Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddHoliday(HolidayRequest request)
        {
            var response = new Response<object>();
            await Task.CompletedTask;

            string id = Guid.NewGuid().ToString();
            Holiday holiday = new Holiday
            {
                HolidayId = id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsDeleted = true
            };
            var result = await _holidayService.AddHoliday(holiday);

            if (result != null)
            {
                response.StatusCode = 200;
                response.Message = "Successfully";
                response.Data = result;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Failed";
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteHoliday(HolidayDelete request)
        {
            var response = new Response<object>();
            string id = request.Id;

            Holiday holiday = await _holidayService.GetHolidaysById(id);

            if (holiday != null)
            {
                var result = await _holidayService.DeleteHoliday(id);
                if (result)
                {
                    response.StatusCode = 200;
                    response.Message = "Successfully";
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "Failed";
                }
            } 
            else
            {
                response.StatusCode = 404;
                response.Message = "Not found";
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateHoliday(HolidayUpdate request)
        {
            var response = new Response<object>();

            Holiday holiday = new Holiday
            {
                HolidayId = request.Id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsDeleted = request.IsDelete
            };

            var result = await _holidayService.UpdateHoliday(holiday);

            if (result)
            {
                response.StatusCode = 200;
                response.Message = "Successfully";
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Failed";
            }
            return Ok(response);
        }
    }
}
