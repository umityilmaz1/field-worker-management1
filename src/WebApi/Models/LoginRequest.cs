using AutoMapper;
using CleanArchitecture.Application.Accounts.Queries.VerifyLoginRequest;

namespace WebApi.Models;

public class LoginRequest
{
    public string Phone { get; set; }
    public string Password { get; set; }

    //public class Mapping : Profile
    //{
    //    public Mapping()
    //    {
    //        CreateMap<LoginRequest, VerifyLoginRequestCommand>();
    //    }
    //}
}
