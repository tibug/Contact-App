using ContactApp.DataAccess.IRepository;
using ContactApp.Models.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactApp.DataAccess.Repository;

public class LookupRepository : ILookupRepository
{
    private readonly string _connectionString;
    private readonly IMemoryCache _cache;
    private readonly ILogger<LookupRepository> _logger;

    public LookupRepository(string connectionString, IMemoryCache cache, ILogger<LookupRepository> logger)
    {
        _connectionString = connectionString;
        _cache = cache;
        _logger = logger;
    }

    private async Task<List<T>> GetCachedDataAsync<T>(string cacheKey, string sqlQuery)
    {
        try
        {
            if (!_cache.TryGetValue(cacheKey, out List<T> data))
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    _logger.LogInformation($"Fetching data for cache key: {cacheKey}");

                    data = (await connection.QueryAsync<T>(sqlQuery)).AsList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) // Cache duration
                    };

                    _cache.Set(cacheKey, data, cacheEntryOptions);
                    _logger.LogInformation($"Data for cache key: {cacheKey} cached successfully.");
                }
            }
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching data for cache key: {cacheKey}");
            throw; // Rethrow the exception to handle it further up the call stack if necessary
        }
    }

    public Task<List<CompanySizes>> GetCompanySizesAsync() =>
        GetCachedDataAsync<CompanySizes>("CompanySizes", "SELECT * FROM CompanySizes");

    public Task<List<RevenueSize>> GetRevenueSizesAsync() =>
        GetCachedDataAsync<RevenueSize>("RevenueSizes", "SELECT * FROM RevenueSizes");

    public Task<List<AccuracyScore>> GetAccuracyScoresAsync() =>
        GetCachedDataAsync<AccuracyScore>("AccuracyScores", "SELECT * FROM AccuracyScores");

    public Task<List<LeadLocation>> GetLeadLocationsAsync() =>
        GetCachedDataAsync<LeadLocation>("LeadLocations", "SELECT * FROM LeadLocations");

    public Task<List<LeadDivision>> GetLeadDivisionsAsync() =>
        GetCachedDataAsync<LeadDivision>("LeadDivisions", "SELECT * FROM LeadDivisions");

    public Task<List<LeadTitle>> GetLeadTitlesAsync() =>
        GetCachedDataAsync<LeadTitle>("LeadTitles", "SELECT * FROM LeadTitles");

    public Task<List<DecisionMakingPowers>> GetDecisionMakingPowersAsync() =>
        GetCachedDataAsync<DecisionMakingPowers>("DecisionMakingPowers", "SELECT * FROM DecisionMakingPowers");

    public Task<List<SeniorityLevels>> GetSeniorityLevelsAsync() =>
        GetCachedDataAsync<SeniorityLevels>("SeniorityLevels", "SELECT * FROM SeniorityLevels");

    public Task<List<CompanyTypes>> GetCompanyTypesAsync() =>
        GetCachedDataAsync<CompanyTypes>("CompanyTypes", "SELECT * FROM CompanyTypes");

    public Task<List<CompanyFunctions>> GetCompanyFunctionsAsync() =>
        GetCachedDataAsync<CompanyFunctions>("CompanyFunctions", "SELECT * FROM CompanyFunctions");

    public Task<List<CompanySectors>> GetCompanySectorsAsync() =>
        GetCachedDataAsync<CompanySectors>("CompanySectors", "SELECT * FROM CompanySectors");

    public Task<List<CompanyIndustries>> GetCompanyIndustriesAsync() =>
        GetCachedDataAsync<CompanyIndustries>("CompanyIndustries", "SELECT * FROM CompanyIndustries");
}
