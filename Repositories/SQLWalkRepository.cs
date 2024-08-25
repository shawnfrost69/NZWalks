using System;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SQLWalkRepository :IWalkRepostiroy
{
    private readonly NZWalksDbContext _dbContext;

    public SQLWalkRepository(NZWalksDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }
}
