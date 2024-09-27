namespace ContactApp.Web.Models
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string CompanyName { get; set; }
        public string Keyword { get; set; }
        public string CompanyIndustry { get; set; }
        public string CompanySize { get; set; }
        public string RevenueRange { get; set; }
        public string SeniorityLevel { get; set; }
        public string LeadDivision { get; set; }
        public string EmailScore { get; set; }
        public string LeadTitles { get; set; }
        public string LeadLocation { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CompanyNAICSCode { get; set; }
        public string CompanySICCode { get; set; }
        public string CompanyProfileImageUrl { get; set; }
        public string LinkedInUrl { get; set; }

        public int PageNumber => (Start / Length) + 1;
        public int PageSize => Length;
    }

}
