using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.SalaryTypeService;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/salary-type")]
    [ApiController]
    [Authorize]
    public class SalaryTypeController : ControllerBase
    {
        private readonly ISalaryTypeService _salaryTypeService;
        private readonly IMapper _mapper;

        public SalaryTypeController(ISalaryTypeService salaryTypeService, IMapper mapper)
        {
            _salaryTypeService = salaryTypeService;
            _mapper = mapper;
        }

      /*  [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var salaryType = await _salaryTypeService.GetAll();

            return Ok(salaryType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            var salaryType = await _salaryTypeService.GetById(id);
            if(salaryType == null)
            {
                return NotFound("Salary type not found");
            }
            else
            {
                return Ok(salaryType);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddSalaryType(SalaryTypeRequest request)
        {
            await Task.CompletedTask;

            string id = Guid.NewGuid().ToString();

            SalaryType salaryType = new SalaryType {
                SalaryTypeId= id,
                SalaryTypeName= request.SalaryTypeName,
                IsDeleted = true
            };
            var result = await _salaryTypeService.AddSalaryType(salaryType);

            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSalaryType(SalaryTypeDelete request)
        {
            string id = request.Id;
            var msg = "";

            SalaryType salaryType = await _salaryTypeService.GetById(id);

            if(salaryType != null)
            {
                var result = await _salaryTypeService.DeleteSalaryType(id);
                if (result)
                {
                    msg = "Delete successfully";
                }
                else
                {
                    msg = "Delete failed";
                }
            }
            else
            {
                msg = "Salary type not found";
            }
            return Ok(msg);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSalaryType(SalaryTypeUpdate request)
        {
            SalaryType salaryType = new SalaryType
            {
                SalaryTypeId = request.Id,
                SalaryTypeName = request.SalaryTypeName,
                IsDeleted = request.IsDelete
            };

            var result = await _salaryTypeService.UpdateSalaryType(salaryType);
            var msg = "";

            if (result)
            {
                msg = "Update successfully";
            }
            else
            {
                return NotFound("Salary type not found");
            }
            return Ok(msg);
        }*/
    }
}
