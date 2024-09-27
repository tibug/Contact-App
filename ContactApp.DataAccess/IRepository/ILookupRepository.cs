using ContactApp.Models.Dtos;

namespace ContactApp.DataAccess.IRepository;

public interface ILookupRepository
{
    Task<List<CompanySizes>> GetCompanySizesAsync();
    Task<List<RevenueSize>> GetRevenueSizesAsync();
    Task<List<AccuracyScore>> GetAccuracyScoresAsync();
    Task<List<LeadLocation>> GetLeadLocationsAsync();
    Task<List<LeadDivision>> GetLeadDivisionsAsync();
    Task<List<LeadTitle>> GetLeadTitlesAsync();
    Task<List<DecisionMakingPowers>> GetDecisionMakingPowersAsync();
    Task<List<SeniorityLevels>> GetSeniorityLevelsAsync();
    Task<List<CompanyTypes>> GetCompanyTypesAsync();
    Task<List<CompanyFunctions>> GetCompanyFunctionsAsync();
    Task<List<CompanySectors>> GetCompanySectorsAsync();
    Task<List<CompanyIndustries>> GetCompanyIndustriesAsync();
}
