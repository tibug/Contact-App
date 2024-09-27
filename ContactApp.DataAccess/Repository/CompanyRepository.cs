using ContactApp.DataAccess.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ContactApp.DataAccess.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly string _connectionString;
    private readonly ILogger<CompanyRepository> _logger;

    public CompanyRepository(string connectionString, ILogger<CompanyRepository> logger)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CompanyProfileDto> GetCompanyProfileByCompanyIdAsync(int companyId)
    {
        try
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "GetCompanyProfileByCompanyId";
                var parameters = new { CompanyId = companyId };

                var companyProfile = await db.QueryFirstOrDefaultAsync<CompanyProfileDto>(
                    query,
                    parameters,
                    commandType: CommandType.StoredProcedure);


                if (companyProfile == null)
                {
                    return null; // or handle not found scenario
                }

                var productsServices = companyProfile.CompanyProductsServices
                                    ?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(p => p.Trim())
                                    .ToList();

                if (productsServices == null || !productsServices.Any())
                {
                    companyProfile.RelatedCompanies = new List<RelatedCompanyDto>();
                    return companyProfile;
                }

                var similarCompanies = await GetRelatedCompanies(companyId, productsServices);

                companyProfile.RelatedCompanies = similarCompanies.AsList();

                return companyProfile;

            }
        }
        catch (SqlException sqlEx)
        {
            _logger.LogError(sqlEx, "An error occurred while retrieving the company profile for CompanyId: {CompanyId}", companyId);
            throw; // Consider throwing a custom exception or rethrowing as needed
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving the company profile.");
            throw;
        }
    }

    private async Task<IEnumerable<RelatedCompanyDto>> GetRelatedCompanies(int companyId, List<string> productServices)
    {
        try
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("Value", typeof(string));
                foreach (var service in productServices)
                {
                    dataTable.Rows.Add(service);
                }
                var parameters = new DynamicParameters();
                parameters.Add("@CompanyId", companyId);
                parameters.Add("@ProductServices", dataTable.AsTableValuedParameter("dbo.StringList"));

                return db.Query<RelatedCompanyDto>("GetRelatedCompanies", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine($"SQL Error: {sqlEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

}
