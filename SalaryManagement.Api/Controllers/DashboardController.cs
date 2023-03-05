using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.CompanyServices;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Application.Services.PaidHistoryServices;
using SalaryManagement.Contracts.Dashboards;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/dashboard")]
    [ApiController]
    [Authorize]

     public class DashboardController : ControllerBase
    {
        private readonly ICompanyServices _companyService;
        private readonly IEmployeeServices _employeeService;
        private readonly IContractServices _cotractService;
        private readonly IPaidHistoryService _paidHistoryService;
        private readonly IMapper _mapper;

        public DashboardController(ICompanyServices companyService, IMapper mapper, 
        IEmployeeServices employeeService, IContractServices contractService, IPaidHistoryService paidHistoryService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _employeeService = employeeService;
            _cotractService = contractService;
            _paidHistoryService = paidHistoryService;
        }

        [HttpGet("total-data")]
        public async Task<IActionResult> getCountData()
        {
            var totalEmp = await _employeeService.CountEmployeeActive();
            var totalCompany = await _companyService.CountCompanyPartner();
            var totalContract = await _cotractService.CountContractActive();
            var totalPayslip = await _paidHistoryService.CountPaySlipsActive();
            var res = new TotalNum(totalEmp, totalCompany, totalContract, totalPayslip);
           return Ok(res);
        }
        [HttpGet("contract-data")]
        public async Task<IActionResult> getContractData()
        {     
            var totalContractActive = await _cotractService.CountContractActive();
            var totalContractExpired = await _cotractService.CountContractExpired();
            var ActiveObject = new KeyValue("Active",totalContractActive);
            var ExpiredObject = new KeyValue("Expired",totalContractExpired);
            var ArrayRes = new KeyValue [] {ActiveObject,ExpiredObject};
           return Ok(ArrayRes);
        }  
    }
        
}