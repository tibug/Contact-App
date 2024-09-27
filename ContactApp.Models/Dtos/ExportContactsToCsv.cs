namespace ContactApp.Models.Dtos;

public class ExportContactsToCsv
{
    public int ContactID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal EmailScore { get; set; }
    public string Phone { get; set; }
    public string WorkPhone { get; set; }
    public string LeadLocation { get; set; }
    public string LeadDivision { get; set; }
    public string LeadTitles { get; set; }
    public string DecisionMakingPower { get; set; }
    public string SeniorityLevel { get; set; }
    public string LinkedinUrl { get; set; }
    public string Skills { get; set; }
    public string PastCompanies { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string CompanySize { get; set; }
    public string CompanyWebsite { get; set; }
    public string CompanyPhoneNumbers { get; set; }
    public string CompanyLocationText { get; set; }
    public string CompanyType { get; set; }
    public string CompanyFunction { get; set; }
    public string CompanySector { get; set; }
    public string CompanyIndustry { get; set; }
    public int? CompanyFoundedAt { get; set; }
    public string CompanyFundingRange { get; set; }
    public string RevenueRange { get; set; }
    public string EbitdaRange { get; set; }
    public string CompanyFacebookPage { get; set; }
    public string CompanyTwitterPage { get; set; }
    public string CompanyLinkedinPage { get; set; }
    public string CompanySicCode { get; set; }
    public string CompanyNaicsCode { get; set; }
    public string CompanyDescription { get; set; }
    public string CompanySizeKey { get; set; }
    public string CompanyProfileImageUrl { get; set; }
}
