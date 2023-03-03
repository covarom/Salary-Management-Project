using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalaryManagement.Application.Common.Exception;
using SalaryManagement.Application.Services.PaidHistoryServices;
using SalaryManagement.Contracts.PaidHistory;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class PaidHistoryController : ControllerBase
    {
        private readonly IPaidHistoryService _paidHistoryService;
        private readonly IMapper _mapper;

        public PaidHistoryController(IPaidHistoryService paidHistoryService, IMapper mapper)
        {
            _paidHistoryService = paidHistoryService;
            _mapper = mapper;
        }

        [HttpGet("paid-history")]
        public async Task<IActionResult> GetPaidHistories(int pageNumber, int pageSize, string? searchKeyword, string? sortBy, bool isDesc)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            var result = await _paidHistoryService.GetPaidHistories(pageNumber, pageSize, sortBy, isDesc, searchKeyword);

            return Ok(result);
        }

        [HttpPost("paid-history")]
        public async Task<IActionResult> SavePaidHistory(PaidHistoryRequest paidHistoryRequest)
        {
            try
            {
                var paidHistory = await _paidHistoryService.SavePaidHistory(paidHistoryRequest);
                var response = paidHistory.Adapt<PaidHistoryResponse>();
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("paid-history/{id}")]
        public async Task<ActionResult<PaidHistoryResponse>> GetById(string id)
        {
            var paidHistory = await _paidHistoryService.GetByIdAsync(id);
            if (paidHistory == null)
            {
                return NotFound();
            }
            return Ok(paidHistory);
        }

        [HttpPut("paid-history/{id}")]
        public async Task<IActionResult> UpdatePaidHistory(string id, [FromBody] PaidHistoryRequest paidHistoryRequest)
        {
            try
            {
                var updatedPaidHistory = await _paidHistoryService.UpdatePaidHistoryAsync(id, paidHistoryRequest);
                var response = _mapper.Map<PaidHistoryResponse>(updatedPaidHistory);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the PaidHistory");
            }
        }

        [HttpDelete("paid-history/{id}")]
        public async Task<IActionResult> DeletePaidHistory(string id)
        {
            try
            {
                await _paidHistoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the paid history.");
            }
        }

    }
}
