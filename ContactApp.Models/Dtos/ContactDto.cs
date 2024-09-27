namespace ContactApp.Models.Dtos;

public class ContactDto
{
    public int ContactId { get; set; }
    public string ContactUniqueId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string EmailScore { get; set; }
    public string Phone { get; set; }
    public string WorkPhone { get; set; }
    public string LeadLocation { get; set; }
    public string LeadDivision { get; set; }
    public string LeadTitle { get; set; }
    public string DecisionMakingPower { get; set; }
    public string SeniorityLevel { get; set; }
    public string LinkedInUrl { get; set; }
    public string Skills { get; set; }
    public string PastCompanies { get; set; }
    public int CompanyId { get; set; }
    public string CompanyUniqueId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyIndustry { get; set; }
    public string CompanySize { get; set; }
    public string RevenueRange { get; set; }
    public string CompanyProfileImageUrl { get; set; }
    public string CompanyDescription { get; set; }
    public string CompanyPhoneNumbers { get; set; }
    public string CompanyWebsite { get; set; }
    public string CompanyFacebookPage { get; set; }
    public string CompanyTwitterPage { get; set; }
}
