using ClosedXML.Excel;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Infrastructure.Persistence.Repositories;
using System.Net.Mail;
using System.Net;
using SalaryManagement.Contracts.Holidays;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1")]
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
            var response = _mapper.Map<List<HolidayResponse>>(holidays);

            return Ok(response);
        }

        [HttpGet("holidays/{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            var holiday = await _holidayService.GetHolidaysById(id);

            if (holiday == null)

            {
                return NotFound("Holiday not found");
            }
            else
            {
                var response = _mapper.Map<HolidayResponse>(holiday);

                return Ok(response);
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
            var response = _mapper.Map<HolidayResponse>(result);

            return Ok(response);
        }

        [HttpDelete("holidays/{id}")]
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

        [HttpPut("holidays/{id}")]
        public async Task<IActionResult> UpdateHoliday(string id, HolidayUpdate request)
        {
            var existHoliday = await _holidayService.GetHolidaysById(id);
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
            var existHoliday = await _holidayService.GetAllHoliday();

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var msg = "";
                try
                {

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {

                            var worksheet = package.Workbook.Worksheets[0];
                            var countRow = worksheet.Dimension.End.Row;

                            

                            //DateTime? previousStartDate = null;
                            for (int row = 2; row <= countRow; row++)
                            {
                                string id = Guid.NewGuid().ToString();
                                string holidayName = worksheet.Cells[row, 1].GetValue<string>();
                                DateTime startDate = worksheet.Cells[row, 2].GetValue<DateTime>();
                                DateTime endDate = worksheet.Cells[row, 3].GetValue<DateTime>();
                                string? input = worksheet.Cells[row, 4].Value.ToString();
                                bool isPaid = false;
                                if (input == "y")
                                {
                                    isPaid = true;
                                }
                                else if (input == "n")
                                {
                                    isPaid = false;
                                }
                                foreach(var holiday in existHoliday)
                                {
                                    if(holiday.StartDate == startDate || holiday.EndDate == endDate || (startDate < holiday.StartDate && endDate > holiday.EndDate))
                                    {
                                        msg += "Import date does exist ";
                                        msg += "\nWrong format at row " + row;
                                        await transaction.RollbackAsync();
                                        return BadRequest("Import failed!!! " + msg);
                                    }
                                }

                                if (endDate < startDate)
                                {
                                    msg += "endDate have to later then startDate ";
                                    msg += "\nWrong format at row " + row;
                                    await transaction.RollbackAsync();
                                    return BadRequest("Import failed!!! " + msg);
                                }
                                else
                                {
                                    Holiday holiday = new Holiday
                                    {
                                        HolidayId = id,
                                        HolidayName = holidayName,
                                        StartDate = startDate,
                                        EndDate = endDate,
                                        IsDeleted = false,
                                        IsPaid = isPaid
                                    };

                                    if (holiday != null)
                                    {
                                        var result = await _holidayService.AddHoliday(holiday);
                                    }
                                    
                                }
                                
                                //previousStartDate = startDate;
                            }
                            await transaction.CommitAsync();
                            return Ok("Import successfully");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest("Import failed!!! " + msg);
                }
            }
        }

        [HttpPost("sendMail")]
        public async Task<IActionResult> sendMail(string toEmail, IFormFile file)
        {
            try
            {
                var email = new MailMessage();
                email.From = new MailAddress("trieudhse151129@fpt.edu.vn");
                email.To.Add(new MailAddress(toEmail));
                email.Subject = "test send mail again";
                email.Body = "Hello world";
                Attachment attachment = new Attachment(file.OpenReadStream(), file.FileName);
                email.Attachments.Add(attachment);

                using var smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential("trieudhse151129@fpt.edu.vn", "01298235295t");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

            
                await smtp.SendMailAsync(email);
                return Ok("Send mail succesfull");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.ToString());
            }
        }
    }
}

