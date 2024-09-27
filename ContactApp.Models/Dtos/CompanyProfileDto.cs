namespace ContactApp.Models.Dtos;

public class CompanyProfileDto
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyDescription { get; set; }
    public string CompanyPhoneNumbers { get; set; }
    public int? CompanyFoundedAt { get; set; }
    public int SeniorityLevelNonManagerId { get; set; }
    public int SeniorityLevelManagerId { get; set; }
    public int SeniorityLevelDirectorId { get; set; }
    public int SeniorityLevelVPId { get; set; }
    public int SeniorityLevelCLevelId { get; set; }
    public int? TotalNonManager { get; set; }
    public int? TotalManagers { get; set; }
    public int? TotalDirectors { get; set; }
    public int? TotalVPLevel { get; set; }
    public int? TotalCLevel { get; set; }
    public string CompanyLinkedinPage { get; set; }
    public string CompanyTwitterPage { get; set; }
    public string CompanyFacebookPage { get; set; }
    public string CompanyProfileImageUrl { get; set; }
    public string CompanyWebsite { get; set; }
    public string CompanyIndustry { get; set; }
    public string CompanySICCode { get; set; }
    public string CompanyNAICSCode { get; set; }
    public string CompanyLocationText { get; set; }
    public string CompanySize { get; set; }
    public string RevenueRange { get; set; }
    public string CompanyProductsServices { get; set; }

    // For related companies
    public List<RelatedCompanyDto> RelatedCompanies { get; set; }
}
