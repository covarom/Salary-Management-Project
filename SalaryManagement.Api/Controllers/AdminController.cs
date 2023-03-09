using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Services.AdminServices;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Admin;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/admin")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        private readonly IMapper _mapper;

        public AdminController(IAdminServices adminServices, IMapper mapper)
        {
            _adminServices = adminServices;
            _mapper = mapper;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {

            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Use the token for further processing.
            var admin = await _adminServices.GetAdminById(JwtTokenHelper.GetClaimValue(token, JwtRegisteredClaimNames.Sub));

            if (admin == null)
            {
                return NotFound();
            }     
            return Ok(admin);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(AdminUpdateRequest rq)
        {
            string id = rq.adminId;
            string updateName = rq.name;
            string image = rq.image;
            string phoneNumber = rq.phoneNumber;
            string email = rq.email;
            string password = rq.password;
            if(rq.adminId == ""){
                return BadRequest();
            }
            var adminExistInfo = await _adminServices.GetAdminById(id);
            if(adminExistInfo == null){
                return NotFound();
            }
            adminExistInfo.Name =  updateName.IsNullOrEmpty() ? adminExistInfo.Name : updateName.Trim();
            adminExistInfo.Image = updateName.IsNullOrEmpty() ?  adminExistInfo.Image : updateName.Trim();  
            adminExistInfo.PhoneNumber = phoneNumber.IsNullOrEmpty() ?  adminExistInfo.PhoneNumber : phoneNumber.Trim();  
            adminExistInfo.Email = email.IsNullOrEmpty() ?  adminExistInfo.Email : email.Trim();  
            if(adminExistInfo.Password == _adminServices.HashPassword(password)){
                return BadRequest("Password have been used!");
            }
            adminExistInfo.Password = password.IsNullOrEmpty() ?  adminExistInfo.Password : password.Trim();  
            var HashPW = _adminServices.HashPassword(adminExistInfo.Password);
            adminExistInfo.Password = HashPW;
            var rs = await _adminServices.UpdateAdmin(adminExistInfo);
            if(!rs){
                  return BadRequest("Update failed!");            
            };
              return Ok("Update successfully") ;
        }

    }

}
