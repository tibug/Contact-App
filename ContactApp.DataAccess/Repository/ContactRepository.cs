using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
namespace ContactApp.DataAccess.Repository;

public class ContactRepository : IContactRepository
{
    private readonly string _connectionString;
    private readonly ILogger<ContactRepository> _logger;

    public ContactRepository(string connectionString, ILogger<ContactRepository> logger)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<(int recordsTotal, int recordsFiltered, IEnumerable<ContactDto> Contacts)> GetContactsAsync(CustomSearchContact request)
    {
        try
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var companyIndustryTable = TableUtility.CreateDataTable(request.company_industry);
                var companySizeTable = TableUtility.CreateDataTable(request.company_size);
                var revenueRangeTable = TableUtility.CreateDataTable(request.revenue_range);
                var seniorityLevelTable = TableUtility.CreateDataTable(request.seniority_level);
                var leadDivisionTable = TableUtility.CreateDataTable(request.lead_division);
                var emailScoreTable = TableUtility.CreateDataTable(request.email_score);

                var parameters = new DynamicParameters();
                parameters.Add("@CompanyName", request.company_name);
                parameters.Add("@Keyword", request.keyword);
                parameters.Add("@CompanyIndustry", companyIndustryTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@CompanySize", companySizeTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@RevenueRange", revenueRangeTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@SeniorityLevel", seniorityLevelTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@LeadDivision", leadDivisionTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@EmailScore", emailScoreTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@LeadTitles", request.lead_titles);
                parameters.Add("@LeadLocation", request.lead_location);
                parameters.Add("@Name", request.name);
                parameters.Add("@Email", request.email);
                parameters.Add("@CompanyNAICSCode", request.company_naics_code);
                parameters.Add("@CompanySICCode", request.company_sic_code);
                parameters.Add("@PageNumber", request.PageNumber);
                parameters.Add("@PageSize", request.PageSize);

                // Execute the stored procedure to fetch contacts and total pages
                using (var multi = await dbConnection.QueryMultipleAsync("SearchContacts", parameters, commandType: CommandType.StoredProcedure))
                {
                    int recordsFiltered = multi.ReadSingle<int>();
                    int recordsTotal = multi.ReadSingle<int>();
                    IEnumerable<ContactDto> contacts = multi.Read<ContactDto>();

                    return (recordsTotal, recordsFiltered, contacts);
                }
            }
        }
        catch (SqlException sqlEx)
        {
            _logger.LogError(sqlEx, "SQL error occurred while fetching contacts");
            throw new Exception("Database error occurred while fetching contacts. Please try again later.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching contacts");
            throw new Exception("An error occurred while fetching contacts. Please try again later.");
        }
    }
    public async Task<IEnumerable<ExportContactsToCsv>> ExportContactsToCsvAsync(CustomSearchContact request, int perExportLimit)
    {
        try
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var companyIndustryTable = TableUtility.CreateDataTable(request.company_industry);
                var companySizeTable = TableUtility.CreateDataTable(request.company_size);
                var revenueRangeTable = TableUtility.CreateDataTable(request.revenue_range);
                var seniorityLevelTable = TableUtility.CreateDataTable(request.seniority_level);
                var leadDivisionTable = TableUtility.CreateDataTable(request.lead_division);
                var emailScoreTable = TableUtility.CreateDataTable(request.email_score);

                var parameters = new DynamicParameters();
                parameters.Add("@CompanyName", request.company_name);
                parameters.Add("@Keyword", request.keyword);
                parameters.Add("@CompanyIndustry", companyIndustryTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@CompanySize", companySizeTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@RevenueRange", revenueRangeTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@SeniorityLevel", seniorityLevelTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@LeadDivision", leadDivisionTable.AsTableValuedParameter("dbo.ContactFilterType"));
                //parameters.Add("@EmailScore", emailScoreTable.AsTableValuedParameter("dbo.ContactFilterType"));
                parameters.Add("@LeadTitles", request.lead_titles);
                parameters.Add("@LeadLocation", request.lead_location);
                parameters.Add("@Name", request.name);
                parameters.Add("@Email", request.email);
                parameters.Add("@CompanyNAICSCode", request.company_naics_code);
                parameters.Add("@CompanySICCode", request.company_sic_code);
                parameters.Add("@PerExportLimit", perExportLimit);

                var contacts = await dbConnection.QueryAsync<ExportContactsToCsv>("ExportContactsToCsv", parameters, commandType: CommandType.StoredProcedure);
                return contacts;
            }
        }
        catch (SqlException sqlEx)
        {
            _logger.LogError(sqlEx, "SQL error occurred while exporting contacts to csv file");
            throw new Exception("Database error occurred while exporting contacts to csv tile. Please try again later.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while export contacts to csv file");
            throw new Exception("An error occurred while exporting contacts to csv file. Please try again later.");
        }
    }
}
