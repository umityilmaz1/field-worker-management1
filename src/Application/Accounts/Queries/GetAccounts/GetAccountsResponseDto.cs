﻿using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Accounts.Queries.GetAccounts;
public class GetAccountsResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsDeleted { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Account, GetAccountsResponseDto>();
        }
    }
}
