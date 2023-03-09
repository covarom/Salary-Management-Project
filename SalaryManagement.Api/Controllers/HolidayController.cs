using ClosedXML.Excel;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Infrastructure.Persistence.Repositories;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    [Authorize]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;
        private readonly SalaryManagementContext _dbContext;

        public HolidayController(IHolidayService holidayService, IMapper mapper, SalaryManagementContext dbContext)
        {
            _holidayService = holidayService;
            _mapper = mapper;
            _dbContext = dbContext;
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
                IsDeleted = false,
                IsPaid = request.IsPaid

            };
            var result = await _holidayService.AddHoliday(holiday);

            return Ok(result);
        }

        [HttpDelete("holidays/{holidayId}")]
        public async Task<IActionResult> DeleteHoliday(HolidayDelete request)
        {
            Holiday holiday = new Holiday
            {
                HolidayId = request.Id,
                IsDeleted = true
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

        [HttpGet("holidays/template")]
        public async Task<IActionResult> GetHolidayExcelTemplate()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HolidayTemplate");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "";
                worksheet.Cell(currentRow, 2).Value = "HolidayName";
                worksheet.Cell(currentRow, 3).Value = "StartDate";
                worksheet.Cell(currentRow, 4).Value = "EndDate";

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "HolidayTemplate.xlsx");
                }
            }

        }

        [HttpPost("holidays/import")]
        public async Task<IActionResult> ImportHolidayFromExcel(IFormFile file)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {

                            var worksheet = package.Workbook.Worksheets[0];
                            var countRow = worksheet.Dimension.End.Row;
                            var holidays = new List<Holiday>();

                            for (int row = 2; row <= countRow; row++)
                            {
                                string id = Guid.NewGuid().ToString();
                                string holidayName = worksheet.Cells[row, 1].GetValue<string>();
                                DateTime startDate = worksheet.Cells[row, 2].GetValue<DateTime>();
                                DateTime endDate = worksheet.Cells[row, 3].GetValue<DateTime>();

                                holidays.Add(new Holiday
                                {
                                    HolidayId = id,
                                    HolidayName = holidayName,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    IsDeleted = false,
                                    IsPaid = false
                                });
                            }
                            bool check = true;
                            foreach (Holiday holiday in holidays)
                            {
                                if (holiday != null)
                                {
                                    var result = await _holidayService.AddHoliday(holiday);
                                    check = true;
                                        if (!check)
                                        {
                                            await transaction.RollbackAsync();
                                            return BadRequest(result);
                                        }
                                }
                            }
                            await transaction.CommitAsync();
                            //Save the holidays to the database
                            //var result = await _holidayService.SaveHoliday(holidays);
                            return Ok("Import successfully");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(ex);
                }
            }
        }
    }
}

