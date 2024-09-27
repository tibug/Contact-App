USE DBContacts;
GO

BEGIN TRANSACTION;

BEGIN TRY

-- Insert data into Contacts
INSERT INTO Contacts (
    UniqueId,
    CompanyID,
    Name,
    Email,
    EmailScore,
    Phone,
    WorkPhone,
    LeadLocationID,
    LeadDivisionID,
    LeadTitle,
    DecisionMakingPowerID,
    SeniorityLevelID,
    LinkedInUrl,
    Skills,
    PastCompanies
)
SELECT
    c.ID AS UniqueId,
    co.Id AS CompanyID,
    c.Name,
    c.Email,
    c.email_score,
    c.Phone,
    c.work_phone,
    ll.Id AS LeadLocationID,
    ld.Id AS LeadDivisionID,
    c.lead_titles,
    dmp.Id AS DecisionMakingPowerID,
    sl.Id AS SeniorityLevelID,
    c.linkedin_url,
    c.Skills,
    c.past_companies
FROM tblContacts AS c
LEFT JOIN Companies co ON co.CompanyName = c.company_name
LEFT JOIN LeadLocations ll ON ll.Location = c.Lead_Location
LEFT JOIN LeadDivisions ld ON ld.Division = c.lead_division
LEFT JOIN DecisionMakingPowers dmp ON dmp.DecisionMakingPower = c.decision_making_power
LEFT JOIN SeniorityLevels sl ON sl.SeniorityLevel = c.seniority_level
WHERE NOT EXISTS (
    SELECT 1
    FROM Contacts AS con
    WHERE con.UniqueId = c.ID
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