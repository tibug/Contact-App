namespace ContactApp.Models;

public class CustomSearchContact
{
    public int Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public int pageLimit { get; set; }

    public string? company_name { get; set; }
    public string? keyword { get; set; }
    public List<int>? company_industry { get; set; }
    public List<int>? company_size { get; set; }
    public List<int>? revenue_range { get; set; }
    public List<int>? seniority_level { get; set; }
    public List<int>? lead_division { get; set; }
    public List<int>? email_score { get; set; }
    public string? lead_titles { get; set; }
    public string? lead_location { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? company_sector { get; set; }
    public int? company_naics_code { get; set; }
    public int? company_sic_code { get; set; }
    public int? PerExportLimit { get; set; }

    public int PageNumber => (Start / Length) + 1;
    public int PageSize => Length;
}
