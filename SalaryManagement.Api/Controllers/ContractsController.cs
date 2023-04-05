using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Contracts;
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
        public async Task<ActionResult<ContractResponse>> AddContract(ContractRequest request)
        {
            var response = await _contractService.AddContractAsync(request);
            return Ok(response);
        }

        [HttpPut("contracts/{id}")]
        public async Task<IActionResult> UpdateContract(string id, [FromBody] ContractRequest request)
        {
            try
            {
                var existingContract = await _contractService.GetContractById(id);

                if (existingContract == null)
                {
                    return NotFound();
                }

                var contract = existingContract;

                if (!string.IsNullOrEmpty(request.File))
                {
                    contract.File = request.File;
                }

                if (request.StartDate.HasValue)
                {
                    contract.StartDate = request.StartDate.Value;
                }

                if (request.EndDate.HasValue)
                {
                    contract.EndDate = request.EndDate.Value;
                }

                if (!string.IsNullOrEmpty(request.Job))
                {
                    contract.Job = request.Job;
                }

                if (request.BasicSalary.HasValue)
                {
                    contract.BasicSalary = request.BasicSalary.Value;
                }

                if (request.Bhxh.HasValue)
                {
                    contract.Bhxh = request.Bhxh.Value;
                }

                if (request.Bhyt.HasValue)
                {
                    contract.Bhyt = request.Bhyt.Value;
                }

                if (request.Bhtn.HasValue)
                {
                    contract.Bhtn = request.Bhtn.Value;
                }

                if (request.Tax.HasValue)
                {
                    contract.Tax = request.Tax.Value;
                }

                if (!string.IsNullOrEmpty(request.PartnerId))
                {
                    contract.PartnerId = request.PartnerId;
                }

                if (request.PartnerPrice.HasValue)
                {
                    contract.PartnerPrice = request.PartnerPrice.Value;
                }

                if (!string.IsNullOrEmpty(request.EmployeeId))
                {
                    contract.EmployeeId = request.EmployeeId;
                }

                if (!string.IsNullOrEmpty(request.SalaryType))
                {
                    contract.SalaryType = request.SalaryType;
                }

                if (!string.IsNullOrEmpty(request.ContractStatus))
                {
                    contract.ContractStatus = request.ContractStatus;
                }

                if (!string.IsNullOrEmpty(request.ContractType))
                {
                    contract.ContractType = request.ContractType;
                }

                await _contractService.UpdateContract(contract);

                return Ok(contract);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the contract.");
            }
        }

        [HttpDelete("contracts/{id}")]
        public async Task<IActionResult> DeleteContract(string id)
        {
            var result = await _contractService.DeleteContractAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }   
}
