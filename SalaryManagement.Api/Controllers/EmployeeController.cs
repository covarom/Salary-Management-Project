using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Contracts.Employee;
using System.Net;

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
            return Ok(Employee);    
        }

        [HttpPost("")]
        public async Task<IActionResult> AddEmployee(EmployeeRequest rq)
        {
            await Task.CompletedTask;
            string id = Guid.NewGuid().ToString();
            Employee Employee = new Employee
            {
                EmployeeId=id,
                     Name = rq.Employee_name ,
                    Image = rq.image,
                    DateOfBirth = rq.day_of_birth,
                    Address = rq.address,
                    IdentifyNumber = rq.identify_number,
                    IsActive= true,
                    PhoneNumber= rq.PhoneNumber
            };


            var result = _EmployeeService.AddEmployee(Employee);
            return Ok(result);
        }
        [HttpPut("update")]
         public async Task<IActionResult> Update(EmployeeUpdate rq)
        {
            string id = rq.id;
            string updateName = rq.Employee_name;
             Employee Employee = new Employee
            {
                EmployeeId= id,
                Name = updateName,
                DateOfBirth = rq.day_of_birth,
                Address = rq.address,
                IdentifyNumber = rq.identify_number,
                IsActive= rq.IsActive,
                PhoneNumber= rq.PhoneNumber
            };
            var rs = await _EmployeeService.UpdateEmployee(Employee);
            var msg ="";
            if(rs){
                    msg = "Update successfully";       
            }else{  
                    msg = "Update failed";            
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