﻿namespace CleanArchitecture.Application.Common.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    string? Token { get; }
}
