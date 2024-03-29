﻿using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Authentication;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationServices authenticationServices, IMapper mapper) {
            _authenticationServices = authenticationServices;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await Task.CompletedTask;

            var authResult = _authenticationServices.Register(request.Name, request.PhoneNumber, request.Username, request.Password);
            AuthenticationResponse response = _mapper.Map<AuthenticationResponse>(authResult); //MapResponse(authResult);

            //var testResponse = new Response<object>(response, (int)HttpStatusCode.OK, "register success");
            
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            await Task.CompletedTask;

            var authResult = _authenticationServices.Login(request.Username, request.Password);
            AuthenticationResponse response = _mapper.Map<AuthenticationResponse>(authResult);

           // var testResponse = new Response<object>(response, (int)HttpStatusCode.OK, "No message");
            return Ok(response);
        }

     /*   [HttpGet("test")]
        public async Task<IActionResult> Test(string a)
        {
            await Task.CompletedTask;
            var response = a;
          //  var testResponse = new Response<object>(response, (int)HttpStatusCode.OK, "Have a message !");
            return Ok(response);
        }*/
    }
}
