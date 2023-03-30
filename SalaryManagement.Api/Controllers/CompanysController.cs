using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Services.CompanyServices;
using SalaryManagement.Application.Services.ContractServices;
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
        private readonly IContractServices _contractService;
        private readonly IMapper _mapper;

        public CompanysController(ICompanyServices companyService, IMapper mapper, IContractServices contractServices)
        {
            _companyService = companyService;
            _mapper = mapper;
            _contractService = contractServices;
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
            if(id == ""){
                return BadRequest("Invalid input");
            }
            var company = await _companyService.GetById(id);
            //  if(!company){
            //      var testResponse = "Không có công ty nào !!!";
            //      return Ok(testResponse);
            // }
            if (company == null)
            {
                return NotFound("Company is not found");
            }
            return Ok(company);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddCompany(CompanyRequest cr)
        {
            await Task.CompletedTask;
            var existCompany = await _companyService.GetAllCompanys();

            var msg = "";
            var company_name = cr.company_name;
            if (company_name == "" || cr.address == "")
            {
                return BadRequest("Invalid input");
            }
            string id = Guid.NewGuid().ToString();
            Company company = new Company
            {
                CompanyId = id,
                CompanyName = company_name,
                Email = cr.email,
                Phone = cr.phone,
                Address = cr.address
            };

            foreach (var com in existCompany)
            {
                if (company.CompanyName.Equals(com.CompanyName) && company.Address.Equals(com.Address))
                {
                    return BadRequest("The company at that address already exists");
                }
            }

            _companyService.AddCompany(company);
            return Ok(msg);

        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(CompanyUpdate cr)
        {
            string id = cr.id;
            string updateName = cr.company_name;
            string updateAddress = cr.company_address;
            string email = cr.email;
            string phone = cr.phone;
            if (cr.id == "")
            {
                return BadRequest("Invalid input");
            }
            var companyExist = await _companyService.GetById(id);
            if (companyExist == null)
            {
                return NotFound("Company is not found");
            }
            companyExist.CompanyName = updateName.IsNullOrEmpty() ? companyExist.CompanyName : updateName.Trim();
            companyExist.Address = updateAddress.IsNullOrEmpty() ? companyExist.Address : updateAddress.Trim();
            companyExist.Email = email.IsNullOrEmpty() ? companyExist.Email : email.Trim();
            companyExist.Phone = phone.IsNullOrEmpty() ? companyExist.Phone : phone.Trim();
            var rs = await _companyService.UpdateCompany(companyExist);
            if (!rs)
            {
                return BadRequest();
            };
            return Ok("Update successfully");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(CompanyDelete cr)
        {
            string id = cr.id;
            var contractList = await _contractService.GetContractByCompanyId(id);
            if (contractList != null)
            {
                return BadRequest("Could not delete from the specified tables");
            }
            var rs = await _companyService.RemoveCompany(id);
            var msg = "";
            if (rs)
            {
                msg = "Delete successfully";
            }
            else
            {
                return BadRequest("Delete failed");
            };
            return Ok(msg);
        }
    }

}