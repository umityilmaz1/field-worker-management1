using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Fields.Queries.GetFields;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Accounts.Queries.GetAccounts;
public class GetAccountsResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsAdmin { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Account, GetAccountsResponseDto>();
        }
    }
}
