use DBContacts
go

CREATE NONCLUSTERED INDEX IX_Contacts_Name ON Contacts(Name);
CREATE NONCLUSTERED INDEX IX_Contacts_Email ON Contacts (Email);
--CREATE NONCLUSTERED INDEX IX_Contacts_EmailScore ON Contacts (EmailScore);
--CREATE NONCLUSTERED INDEX IX_Contacts_SeniorityLevelId ON Contacts (SeniorityLevelId);
--CREATE NONCLUSTERED INDEX IX_Contacts_LeadDivisionId ON Contacts (LeadDivisionId);
CREATE NONCLUSTERED INDEX IX_Contacts_LeadTitle ON Contacts (LeadTitle);
--CREATE NONCLUSTERED INDEX IX_Contacts_LeadLocationId ON Contacts (LeadLocationId);
--CREATE NONCLUSTERED INDEX IX_Contacts_DecisionMakingPowerID ON Contacts (DecisionMakingPowerID);

--CREATE NONCLUSTERED INDEX IX_Companies_CompanyName ON Companies (CompanyName);
CREATE NONCLUSTERED INDEX IX_Companies_CompanyNAICSCode ON Companies (CompanyNAICSCode);
CREATE NONCLUSTERED INDEX IX_Companies_CompanySICCode ON Companies (CompanySICCode);
--CREATE NONCLUSTERED INDEX IX_Companies_CompanyIndustry ON Companies (CompanyIndustryId);
--CREATE NONCLUSTERED INDEX IX_Companies_CompanySize ON Companies (CompanySizeId);
--CREATE NONCLUSTERED INDEX IX_Companies_RevenueRange ON Companies (RevenueRange);

CREATE NONCLUSTERED INDEX IX_Companies_Filter ON Companies(CompanyIndustryID, CompanySizeID, RevenueRange, CompanyName);
CREATE NONCLUSTERED INDEX IX_Contacts_Filter ON Contacts(CompanyId, SeniorityLevelID, LeadDivisionID, LeadLocationId, DecisionMakingPowerId, EmailScore);


CREATE NONCLUSTERED INDEX IX_AccuracyScores_Score ON AccuracyScores (Score);
CREATE NONCLUSTERED INDEX IX_CompanyFunctions_CompanyFunction ON CompanyFunctions (CompanyFunction);
CREATE NONCLUSTERED INDEX IX_CompanyIndustries_CompanyIndustry ON CompanyIndustries (CompanyIndustry);
CREATE NONCLUSTERED INDEX IX_CompanySectors_CompanySector ON CompanySectors (CompanySector);
CREATE NONCLUSTERED INDEX IX_CompanySizes_CompanySize ON CompanySizes (CompanySize);
CREATE NONCLUSTERED INDEX IX_CompanyTypes_CompanyType ON CompanyTypes (CompanyType);

CREATE NONCLUSTERED INDEX IX_DecisionMakingPowers_DecisionMakingPower ON DecisionMakingPowers (DecisionMakingPower);
CREATE NONCLUSTERED INDEX IX_LeadDivisions_Division ON LeadDivisions (Division);
CREATE NONCLUSTERED INDEX IX_LeadLocations_Location ON LeadLocations (Location);

CREATE NONCLUSTERED INDEX IX_RevenueSizes_RevenueRange ON RevenueSizes (RevenueRange);
CREATE NONCLUSTERED INDEX IX_SeniorityLevels_SeniorityLevel ON SeniorityLevels (SeniorityLevel);

CREATE UNIQUE CLUSTERED INDEX IX_vw_ContactCounts ON dbo.vw_ContactCounts(RecordsTotal);
