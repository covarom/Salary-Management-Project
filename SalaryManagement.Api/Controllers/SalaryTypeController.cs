using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using SalaryManagement.Application.Services.SalaryTypeService;
using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/salaryType")]
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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response = new Response<object>();
            var salaryType = await _salaryTypeService.GetAll();
            if (salaryType != null)
            {
                response.StatusCode = 200;
                response.Message = "Successfully";
                response.Data = salaryType;
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
            var salaryType = await _salaryTypeService.GetById(id);
            if (salaryType != null)
            {
                response.StatusCode = 200;
                response.Message = "Successfully";
                response.Data = salaryType;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Not found";
            }
            return Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddSalaryType(SalaryTypeRequest request)
        {
            var response = new Response<object>();
            await Task.CompletedTask;

            string id = Guid.NewGuid().ToString();

            SalaryType salaryType = new SalaryType {
                SalaryTypeId= id,
                SalaryTypeName= request.SalaryTypeName,
                IsDeleted = true
            };
            var result = await _salaryTypeService.AddSalaryType(salaryType);

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
        public async Task<IActionResult> DeleteSalaryType(SalaryTypeDelete request)
        {
            var response = new Response<object>();
            string id = request.Id;

            SalaryType salaryType = await _salaryTypeService.GetById(id);

            if(salaryType != null)
            {
                var result = await _salaryTypeService.DeleteSalaryType(id);
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
        public async Task<IActionResult> UpdateSalaryType(SalaryTypeUpdate request)
        {
            var response = new Response<object>();

            SalaryType salaryType = new SalaryType
            {
                SalaryTypeId = request.Id,
                SalaryTypeName = request.SalaryTypeName,
                IsDeleted = request.IsDelete
            };

            var result = await _salaryTypeService.UpdateSalaryType(salaryType);


            if(result)
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
