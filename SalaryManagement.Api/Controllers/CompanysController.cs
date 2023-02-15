using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.CompanyServices;
using SalaryManagement.Contracts.Companys;
using SalaryManagement.Domain.Entities;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/companys")]
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

        [HttpGet("getAllCompanys")]
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
            return Ok(company);    
        }

        [HttpPost("addCompany")]
        public async Task<IActionResult> AddContract(CompanyRequest cr)
        {
            await Task.CompletedTask;


            var company_name = cr.company_name ;
            string id = Guid.NewGuid().ToString();
            Company company = new Company
            {
                CompanyId= id,
                CompanyName = company_name
            };


            var result = _companyService.AddCompany(company);
            return Ok(result);
        }
    }
        
}