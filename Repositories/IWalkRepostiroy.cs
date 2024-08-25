using System;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public interface IWalkRepostiroy
{
    Task<Walk> CreateAsync(Walk walk);
}
