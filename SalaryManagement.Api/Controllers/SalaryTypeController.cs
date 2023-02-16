using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.SalaryTypeService;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/salaryType")]
    [ApiController]
    public class SalaryTypeController : ControllerBase
    {
        private readonly ISalaryTypeService _salaryTypeService;
        private readonly IMapper _mapper;

        public SalaryTypeController(ISalaryTypeService salaryTypeService, IMapper mapper)
        {
            _salaryTypeService = salaryTypeService;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var salaryType = await _salaryTypeService.GetAll();
            return Ok(salaryType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            var salaryType = await _salaryTypeService.GetById(id);
            return Ok(salaryType);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddSalaryType(SalaryType salaryType)
        {
            var result = await _salaryTypeService.AddSalaryType(salaryType);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryType(string id)
        {
            var result = await _salaryTypeService.DeleteSalaryType(id);
            if (result is null)
            {
                return NotFound("Holiday not found!!!");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalaryType(string id, SalaryType request)
        {
            var result = await _salaryTypeService.UpdateSalaryType(id, request);
            if (result is null)
            {
                return NotFound("Holiday not found!!!");
            }
            return Ok(result);
        }
    }
}
