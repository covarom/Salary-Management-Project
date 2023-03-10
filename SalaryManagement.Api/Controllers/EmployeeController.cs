using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Contracts.Employee;
using System.Net;
using SalaryManagement.Api.Common.Helper;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/employees")]
    [ApiController]
    [Authorize]

     public class EmployeeController : ControllerBase
    {
         private readonly IEmployeeServices _EmployeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeServices EmployeeService, IMapper mapper)
        {
            _EmployeeService = EmployeeService;
            _mapper = mapper;
        }

        [HttpGet("all")]
         public async Task<IActionResult> GetAll()
        {
            var Employee = await _EmployeeService.GetAllEmployees();
            
            return Ok(Employee);    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(string id)
        {
            var Employee = await _EmployeeService.GetById(id);
            //  if(!Employee){
            //      var testResponse = "Không có công ty nào !!!";
            //      return Ok(testResponse);
            // }

             if (Employee == null)
            {
                return NotFound();
            }

            return Ok(Employee);    
        }

        [HttpPost("")]
        public async Task<IActionResult> AddEmployee(EmployeeRequest rq)
        {
            await Task.CompletedTask;
            string id = Guid.NewGuid().ToString();
            int codeEmp = await _EmployeeService.CountEmployee();
            Employee Employee = new Employee
            {
                EmployeeId=id,
                Name = rq.Employee_name ,
                Image = rq.image,
                DateOfBirth = rq.day_of_birth,
                Code = "NV" + codeEmp.ToString(),
                Address = rq.address,
                IdentifyNumber = rq.identify_number,
                IsActive= true,
                PhoneNumber= rq.PhoneNumber,
                Email = rq.Email
            };


            var result = _EmployeeService.AddEmployee(Employee);
            return Ok(result);
        }
        [HttpPut("update")]
         public async Task<IActionResult> Update(EmployeeUpdate rq)
            {
            string id = rq.Id;
            if(id == ""){
                return BadRequest();
            }
            var employeesExist = await _EmployeeService.GetById(id);
            if(employeesExist == null){
                return NotFound();
            }
             employeesExist.Name =  rq.Employee_name.IsNullOrEmpty() ?employeesExist.Name : rq.Employee_name.Trim();
             employeesExist.Address =  rq.Address.IsNullOrEmpty() ? employeesExist.Address : rq.Address.Trim(); 
             employeesExist.Image =  rq.Image.IsNullOrEmpty() ? employeesExist.Image : rq.Image.Trim(); 
             employeesExist.DateOfBirth =  rq.Day_of_birth.HasValue ? rq.Day_of_birth: employeesExist.DateOfBirth ; 
             employeesExist.IsActive =  rq.IsActive.HasValue ?  rq.IsActive : employeesExist.IsActive ; 
             employeesExist.IdentifyNumber = StringHelper.IsNullOrEmpty(rq.Identify_number.ToString()) ? employeesExist.IdentifyNumber : rq.Identify_number; 
             employeesExist.PhoneNumber =  rq.PhoneNumber.IsNullOrEmpty() ? employeesExist.PhoneNumber : rq.PhoneNumber.Trim();
             employeesExist.Email = rq.Email.IsNullOrEmpty() ? employeesExist.Email : rq.Email.Trim();
            var rs = await _EmployeeService.UpdateEmployee(employeesExist);
            var msg ="";
            if(rs){
                    msg = "Update successfully";       
            }else{  
                return NotFound();
            };
            return Ok(msg);    
        }

        [HttpDelete("delete")]
         public async Task<IActionResult> Delete(EmployeeDelete cr)
        {
            string id = cr.id;
            var rs = await _EmployeeService.RemoveEmployee(id);
            var msg ="";
            if(rs){
                    msg = "Delete successfully";       
            }else{  
                    msg = "Delete failed";            
            };
            return Ok(msg);      
        }
    }
        
}