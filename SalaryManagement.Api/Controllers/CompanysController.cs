using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Services.CompanyServices;
using SalaryManagement.Contracts.Companys;
using SalaryManagement.Domain.Entities;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/companies")]
    [ApiController]
    [Authorize]

     public class CompanysController : ControllerBase
    {
        private readonly ICompanyServices _companyService;
        private readonly IMapper _mapper;

        public CompanysController(ICompanyServices companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("all")]
         public async Task<IActionResult> GetAll()
        {
            var company = await _companyService.GetAllCompanys();
            
            return Ok(company);    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(string id)
        {
            var company = await _companyService.GetById(id);
            //  if(!company){
            //      var testResponse = "Không có công ty nào !!!";
            //      return Ok(testResponse);
            // }
             if (company == null)
            {
                return NotFound();
            }
            return Ok(company);    
        }

        [HttpPost("")]
        public async Task<IActionResult> AddContract(CompanyRequest cr)
        {
            await Task.CompletedTask;


            var company_name = cr.company_name ;
            if(company_name =="" || cr.address ==""){
                return BadRequest();
            }
            string id = Guid.NewGuid().ToString();
            Company company = new Company
            {
                CompanyId= id,
                CompanyName = company_name,
                Address = cr.address
            };


            var result = _companyService.AddCompany(company);
            return Ok(result);
        }
        [HttpPut("update")]
         public async Task<IActionResult> Update(CompanyUpdate cr)
        {
            string id = cr.id;
            string updateName = cr.company_name;
            string updateAddress = cr.company_address;
            if(cr.id == ""){
                return BadRequest();
            }
            var companyExist = await _companyService.GetById(id);
            if(companyExist == null){
                return NotFound();
            }
            companyExist.CompanyName =  updateName.IsNullOrEmpty() ?companyExist.CompanyName : updateName.Trim();
             companyExist.Address = updateAddress.IsNullOrEmpty() ? companyExist.Address : updateAddress.Trim();   
            var rs = await _companyService.UpdateCompany(companyExist);
            if(!rs){
                  return BadRequest();            
            };
              return Ok("Update successfully") ;
        }

        [HttpDelete("delete")]
         public async Task<IActionResult> Delete(CompanyDelete cr)
        {
            string id = cr.id;
            var rs = await _companyService.RemoveCompany(id);
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