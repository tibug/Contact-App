
USE DBContacts;
GO

BEGIN TRANSACTION;

BEGIN TRY
-- Insert data into LeadLocations
INSERT INTO LeadLocations (Location)
SELECT DISTINCT lead_location
FROM tblContacts
WHERE lead_location IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM LeadLocations 
    WHERE Location = tblContacts.lead_location
);

-- Insert data into CompanySizes
INSERT INTO CompanySizes (CompanySize)
SELECT DISTINCT company_size
FROM tblContacts
WHERE company_size IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM CompanySizes 
    WHERE CompanySize = tblContacts.company_size
);

-- Insert data into LeadDivisions
INSERT INTO LeadDivisions (Division)
SELECT DISTINCT lead_division
FROM tblContacts
WHERE lead_division IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM LeadDivisions 
    WHERE Division = tblContacts.lead_division
);

-- Insert data into LeadTitles
INSERT INTO LeadTitles (Title)
SELECT DISTINCT lead_titles
FROM tblContacts
WHERE lead_titles IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM LeadTitles 
    WHERE Title = tblContacts.lead_titles
);

-- Insert data into DecisionMakingPowers
INSERT INTO DecisionMakingPowers (DecisionMakingPower)
SELECT DISTINCT decision_making_power
FROM tblContacts
WHERE decision_making_power IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM DecisionMakingPowers 
    WHERE DecisionMakingPower = tblContacts.decision_making_power
);

-- Insert data into SeniorityLevels
INSERT INTO SeniorityLevels (SeniorityLevel)
SELECT DISTINCT seniority_level
FROM tblContacts
WHERE seniority_level IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM SeniorityLevels 
    WHERE SeniorityLevel = tblContacts.seniority_level
);

-- Insert data into CompanyTypes
INSERT INTO CompanyTypes (CompanyType)
SELECT DISTINCT company_type
FROM tblContacts
WHERE company_type IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM CompanyTypes 
    WHERE CompanyType = tblContacts.company_type
);

-- Insert data into CompanyFunctions
INSERT INTO CompanyFunctions (CompanyFunction)
SELECT DISTINCT company_function
FROM tblContacts
WHERE company_function IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM CompanyFunctions 
    WHERE CompanyFunction = tblContacts.company_function
);

-- Insert data into CompanySectors
INSERT INTO CompanySectors (CompanySector)
SELECT DISTINCT company_sector
FROM tblContacts
WHERE company_sector IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM CompanySectors 
    WHERE CompanySector = tblContacts.company_sector
);

-- Insert data into CompanyIndustries
INSERT INTO CompanyIndustries (CompanyIndustry)
SELECT DISTINCT company_industry
FROM tblContacts
WHERE company_industry IS NOT NULL
AND NOT EXISTS (
    SELECT 1 
    FROM CompanyIndustries 
    WHERE CompanyIndustry = tblContacts.company_industry
);

-- Commit transaction if all statements succeed
    COMMIT;
END TRY
BEGIN CATCH
    -- Rollback transaction in case of error
    ROLLBACK;

    -- Output error message
    SELECT 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_MESSAGE() AS ErrorMessage,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState;
END CATCH;

Go