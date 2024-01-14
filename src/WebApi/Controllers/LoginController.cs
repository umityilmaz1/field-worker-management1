using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CleanArchitecture.Application.Accounts.Queries.VerifyLoginRequest;
using CleanArchitecture.Model.Commons;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace WebApi.Controllers;
public class LoginController : ApiControllerBase
{
    private IConfiguration _config;
    private IMapper _mapper;
    public LoginController(IConfiguration config, IMapper mapper)
    {
        _config = config;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ReturnData<string?>> Login([FromBody] LoginRequest loginRequest)
    {
        var loginRequestVerified = await Mediator.Send(new VerifyLoginRequestCommand { Phone = loginRequest.Phone, Password = loginRequest.Password });

        if (loginRequestVerified.Data == null)
        {
            return ReturnData<string?>.Fail();
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          new List<Claim> { new Claim(JwtRegisteredClaimNames.Name, loginRequestVerified.Data.Name), new Claim(JwtRegisteredClaimNames.FamilyName, loginRequestVerified.Data.Surname), new Claim("IsAdmin", loginRequestVerified.Data.IsAdmin.ToString()), new Claim("AccountId", loginRequestVerified.Data.Id.ToString()) },
          expires: DateTime.Now.AddDays(7),
          signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
        return ReturnData<string?>.Success(token);
    }
}