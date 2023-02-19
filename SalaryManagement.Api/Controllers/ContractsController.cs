using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Authentication;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Contracts;
using SalaryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class ContractsController : ControllerBase
    {
        private readonly IContractServices _contractService;
        private readonly IMapper _mapper;

        public ContractsController(IContractServices contractService, IMapper mapper)
        {
            _contractService = contractService;
            _mapper = mapper;
        }

        [HttpGet("contracts")]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string? searchKeyword, string? sortBy, bool isDesc)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            var contracts = await _contractService.GetAllContracts(pageNumber, pageSize, sortBy, isDesc, searchKeyword);
           // var response = contracts.Adapt<List<ContractResponse>>();

            return Ok(contracts);
        }

        [HttpGet("contracts/{id}")]
        public async Task<IActionResult> Find(string id)
        {
            var contracts = await _contractService.GetById(id);
            if (contracts == null)
            {
                return NotFound();
            }

           // var response = _mapper.Map<ContractResponse>(contracts); 

            return Ok(contracts);
        }

        /*

        [HttpPost("contracts")]
        public async Task<IActionResult> AddContract([FromBody]ContractRequest request)
        {
            await Task.CompletedTask;
            var contract = new Contract
            {
                ContractId = Guid.NewGuid().ToString(),
                File = request.File,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Job = request.Job,
                BasicSalary = request.BasicSalary,
                Bhxh = request.Bhxh,
                Bhyt = request.Bhyt,
                Bhtn = request.Bhtn,
                Tax = request.Tax,
                PartnerId = request.PartnerId,
                PartnerPrice = request.PartnerPrice,
                EmployeeId = request.EmployeeId,
                ContractType = request.ContractType,
                SalaryType = request.SalaryType,
                ContractStatus = request.ContractStatus
            };

            var addedContract = await _contractService.AddContractAsync(contract);

            return CreatedAtAction(nameof(Find), new { id = contract.ContractId }, addedContract);

        }

        [HttpPut("contracts/{id}")]
        public async Task<IActionResult> UpdateContract(string id, [FromBody] ContractRequest request)
        {
            await Task.CompletedTask;
            var contract = new Contract
            {
                File = request.File,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Job = request.Job,
                BasicSalary = request.BasicSalary,
                Bhxh = request.Bhxh,
                Bhyt = request.Bhyt,
                Bhtn = request.Bhtn,
                Tax = request.Tax,
                PartnerId = request.PartnerId,
                PartnerPrice = request.PartnerPrice,
                EmployeeId = request.EmployeeId,
                ContractType = request.ContractType,
                SalaryType = request.SalaryType,
                ContractStatus = request.ContractStatus
            };


            var updatedContract = await _contractService.UpdateContractAsync(id, contract);

            if (updatedContract == null)
            {
                return NotFound();
            }

            return Ok(updatedContract);
        }


        [HttpDelete("contract/{id}")]
        public async Task<IActionResult> DeleteContract(string id)
        {
            await _contractService.DeleteContractAsync(id);

            return NoContent();
        }*/

    }   
}
