using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Entities;
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
        public async Task<IActionResult> GetAll()
        {
            var contracts = await _contractService.GetAllContracts();

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

            return Ok(contracts);
        }

        [HttpPost("contracts")]
        public async Task<IActionResult> AddContract([FromBody]SaveContractRequest request)
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
                Bhxh = request.BHXH,
                Partner = request.Partner,
                PartnerPrice = request.PartnerPrice,
                EmployeeId = request.EmployeeId,
                ContractTypeId = request.ContractTypeId,
                SalaryTypeId = request.SalaryTypeId,
                ContractStatusId = request.ContractStatusId
            };

            var addedContract = await _contractService.AddContractAsync(contract);

            return CreatedAtAction(nameof(Find), new { id = contract.ContractId }, addedContract);

        }


        [HttpDelete("contract/{id}")]
        public async Task<IActionResult> DeleteContract(string id)
        {
            await _contractService.DeleteContractAsync(id);

            return NoContent();
        }

    }   
}
