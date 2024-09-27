namespace ContactApp.DataAccess.IRepository;

public interface ICompanyRepository
{
    Task<CompanyProfileDto> GetCompanyProfileByCompanyIdAsync(int companyId);
}
