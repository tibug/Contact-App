namespace ContactApp.Models.Dtos;

public class RevenueSize
{
    public int Id { get; set; }
    public string RevenueRange { get; set; }
}

public class AccuracyScore
{
    public int Id { get; set; }
    public int Score { get; set; }
}

public class LeadLocation
{
    public int Id { get; set; }
    public string Location { get; set; }
}

public class LeadDivision
{
    public int Id { get; set; }
    public string Division { get; set; }
    public string Heading { get; set; }
}

public class LeadTitle
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public class DecisionMakingPowers
{
    public int Id { get; set; }
    public string DecisionMakingPower { get; set; }
}

public class SeniorityLevels
{
    public int Id { get; set; }
    public string SeniorityLevel { get; set; }
}

public class CompanyTypes
{
    public int Id { get; set; }
    public string CompanyType { get; set; }
}

public class CompanyFunctions
{
    public int Id { get; set; }
    public string CompanyFunction { get; set; }
}

public class CompanySectors
{
    public int Id { get; set; }
    public string CompanySector { get; set; }
}

public class CompanyIndustries
{
    public int Id { get; set; }
    public string CompanyIndustry { get; set; }
    public string Heading { get; set; }
}

public class CompanySizes
{
    public int Id { get; set; }
    public string CompanySize { get; set; }
}

public class JobCategory
{
    public string CategoryName { get; set; }
    public List<LeadDivision> Titles { get; set; }
}
public class IndustryCategory
{
    public string CategoryName { get; set; }
    public List<CompanyIndustries> Industries { get; set; }
}