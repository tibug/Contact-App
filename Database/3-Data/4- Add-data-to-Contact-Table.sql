USE DBContacts;
GO

BEGIN TRANSACTION;

BEGIN TRY
    -- Insert data into Contacts ensuring one-to-one relationships in joins
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
    LEFT JOIN (
        SELECT CompanyName, MIN(Id) AS Id
        FROM Companies
        GROUP BY CompanyName
    ) co ON co.CompanyName = c.company_name
    LEFT JOIN (
        SELECT Location, MIN(Id) AS Id
        FROM LeadLocations
        GROUP BY Location
    ) ll ON ll.Location = c.Lead_Location
    LEFT JOIN (
        SELECT Division, MIN(Id) AS Id
        FROM LeadDivisions
        GROUP BY Division
    ) ld ON ld.Division = c.lead_division
    LEFT JOIN (
        SELECT DecisionMakingPower, MIN(Id) AS Id
        FROM DecisionMakingPowers
        GROUP BY DecisionMakingPower
    ) dmp ON dmp.DecisionMakingPower = c.decision_making_power
    LEFT JOIN (
        SELECT SeniorityLevel, MIN(Id) AS Id
        FROM SeniorityLevels
        GROUP BY SeniorityLevel
    ) sl ON sl.SeniorityLevel = c.seniority_level
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

GO