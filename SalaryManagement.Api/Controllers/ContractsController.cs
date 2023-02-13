using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Contracts.Contracts;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
  
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
            return Ok(contracts);
        }

        [HttpPost("contracts")]
        public async Task<IActionResult> AddContract(ContractRequest request)
        {
            await Task.CompletedTask;
            /*
             Do the bussiness logic
             */

            //var contracts = await _contractService.GetById(id);
            return Ok(request);
        }

    }
}
