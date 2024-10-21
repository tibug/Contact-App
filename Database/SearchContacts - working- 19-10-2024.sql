ALTER PROCEDURE [dbo].[SearchContacts]    
    @CompanyName NVARCHAR(255) = NULL,  
    @CompanySize dbo.ContactFilterType READONLY,  
    @Name NVARCHAR(255) = NULL,    
    @Email NVARCHAR(255) = NULL,   
    @EmailScore dbo.ContactFilterType READONLY,   
    @LeadLocation NVARCHAR(255) = NULL,   
    @LeadDivision dbo.ContactFilterType READONLY,    
    @SeniorityLevel dbo.ContactFilterType READONLY,   
    @CompanySector NVARCHAR(255) = NULL,   
    @RevenueRange dbo.ContactFilterType READONLY,    
    @CompanyIndustry dbo.ContactFilterType READONLY,  
    @LeadTitle NVARCHAR(255) = NULL,  
    @CompanyNAICSCode INT = NULL,    
    @CompanySICCode INT = NULL,   
    @Keyword NVARCHAR(255) = NULL,  
    @PageNumber INT = 1,    
    @PageSize INT = 25    
AS    
BEGIN    
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @WhereClauseContacts NVARCHAR(MAX) = '1=1'; -- Contacts filtering
    DECLARE @WhereClauseCompanies NVARCHAR(MAX) = '1=1'; -- Companies filtering

    -- Step 1: Filter conditions for Contacts table
    IF @Name IS NOT NULL AND @Name != ''
        SET @WhereClauseContacts = @WhereClauseContacts + ' AND c.Name LIKE ''%' + @Name + '%''';
    IF @Email IS NOT NULL AND @Email !=''
        SET @WhereClauseContacts = @WhereClauseContacts + ' AND c.Email LIKE ''%' + @Email + '%''';
    IF @LeadTitle IS NOT NULL AND @LeadTitle !=''
        SET @WhereClauseContacts = @WhereClauseContacts + ' AND c.LeadTitle LIKE ''%' + @LeadTitle + '%''';
    IF @LeadLocation IS NOT NULL AND @LeadLocation !=''
        SET @WhereClauseContacts = @WhereClauseContacts + ' AND c.LeadLocationID IN (SELECT l.Id FROM LeadLocations l WHERE l.Location LIKE ''%' + @LeadLocation + '%'')';
    IF EXISTS (SELECT 1 FROM @LeadDivision)
        SET @WhereClauseContacts = @WhereClauseContacts + ' AND c.LeadDivisionID IN (SELECT Value FROM @LeadDivision)';
    IF EXISTS (SELECT 1 FROM @SeniorityLevel)
        SET @WhereClauseContacts = @WhereClauseContacts + ' AND c.SeniorityLevelID IN (SELECT Value FROM @SeniorityLevel)';
    IF EXISTS (SELECT 1 FROM @EmailScore)
        SET @WhereClauseContacts = @WhereClauseContacts + ' AND c.EmailScore IN (SELECT Value FROM @EmailScore)';

    -- Step 2: Filter conditions for Companies table (separate conditions)
    IF @CompanyName IS NOT NULL AND @CompanyName != ''
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.CompanyName LIKE ''%' + @CompanyName + '%''';
    IF @Keyword IS NOT NULL AND @Keyword != ''
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.keywords LIKE ''%' + @Keyword + '%''';
    IF EXISTS (SELECT 1 FROM @CompanyIndustry)
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.CompanyIndustryID IN (SELECT Value FROM @CompanyIndustry)';
    IF EXISTS (SELECT 1 FROM @CompanySize)
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.CompanySizeID IN (SELECT Value FROM @CompanySize)';
    IF EXISTS (SELECT 1 FROM @RevenueRange)
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.RevenueRange IN (SELECT Value FROM @RevenueRange)';
    IF @CompanySector IS NOT NULL AND @CompanySector != ''
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.CompanySectorId IN (SELECT s.Id FROM CompanySectors s WHERE s.CompanySector LIKE ''%' + @CompanySector + '%'')';
    IF @CompanyNAICSCode IS NOT NULL
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.CompanyNAICSCode = @CompanyNAICSCode';
    IF @CompanySICCode IS NOT NULL
        SET @WhereClauseCompanies = @WhereClauseCompanies + ' AND co.CompanySICCode = @CompanySICCode';

    -- Step 3: Build the main query
    SET @SQL = '
    WITH FilteredContacts AS (
        SELECT c.CompanyID
        FROM Contacts c WITH(NOLOCK)
        WHERE ' + @WhereClauseContacts + '
    )
    SELECT COUNT_BIG(1) AS RecordsFiltered
    FROM FilteredContacts fc
    INNER JOIN Companies co WITH(NOLOCK) ON fc.CompanyID = co.Id
    WHERE ' + @WhereClauseCompanies + ';

    SELECT RecordsTotal FROM dbo.vw_ContactCounts;  

    SELECT c.Id AS ContactID, c.UniqueId AS ContactUniqueId, c.Name, c.Email, c.EmailScore, c.Phone, 
           c.WorkPhone, l.Location AS LeadLocation, d.Division AS LeadDivision, c.LeadTitle, 
           p.DecisionMakingPower, s.SeniorityLevel,
           CASE 
                WHEN c.LinkedInUrl IS NULL OR c.LinkedInUrl = '''' THEN NULL 
                WHEN LEFT(c.LinkedInUrl, 4) <> ''http'' THEN ''https://'' + c.LinkedInUrl 
                ELSE c.LinkedInUrl 
           END AS LinkedInUrl,
           c.Skills, c.PastCompanies, co.Id AS CompanyId, co.CompanyName, ci.CompanyIndustry, 
           cs.CompanySize, co.RevenueRange, co.CompanyProfileImageUrl, co.CompanyNAICSCode, 
           co.CompanySICCode, co.CompanyDescription, co.CompanyPhoneNumbers, 
           CASE 
                WHEN co.CompanyWebsite IS NULL OR co.CompanyWebsite = '''' THEN NULL 
                WHEN LEFT(co.CompanyWebsite, 4) <> ''http'' THEN ''https://'' + co.CompanyWebsite 
                ELSE co.CompanyWebsite 
           END AS CompanyWebsite, 
           CASE 
                WHEN co.CompanyFacebookPage IS NULL OR co.CompanyFacebookPage = '''' THEN NULL 
                WHEN LEFT(co.CompanyFacebookPage, 4) <> ''http'' THEN ''https://'' + co.CompanyFacebookPage 
                ELSE co.CompanyFacebookPage 
           END AS CompanyFacebookPage, 
           CASE 
                WHEN co.CompanyTwitterPage IS NULL OR co.CompanyTwitterPage = '''' THEN NULL 
                WHEN LEFT(co.CompanyTwitterPage, 4) <> ''http'' THEN ''https://'' + co.CompanyTwitterPage 
                ELSE co.CompanyTwitterPage 
           END AS CompanyTwitterPage, 
           CASE 
                WHEN co.CompanyProfileImageUrl IS NULL OR co.CompanyProfileImageUrl = '''' THEN NULL 
                WHEN LEFT(co.CompanyProfileImageUrl, 4) <> ''http'' THEN ''https://'' + co.CompanyProfileImageUrl 
                ELSE co.CompanyProfileImageUrl 
           END AS CompanyProfileImageUrl
    FROM Contacts c
    INNER JOIN LeadLocations l ON c.LeadLocationID = l.Id
    INNER JOIN LeadDivisions d ON c.LeadDivisionID = d.Id
    INNER JOIN DecisionMakingPowers p ON c.DecisionMakingPowerID = p.Id
    INNER JOIN SeniorityLevels s ON c.SeniorityLevelID = s.Id
    INNER JOIN Companies co ON c.CompanyID = co.Id
    INNER JOIN CompanyIndustries ci ON co.CompanyIndustryID = ci.Id
    LEFT JOIN CompanySizes cs ON co.CompanySizeID = cs.Id
    WHERE ' + @WhereClauseContacts + ' 
    AND ' + @WhereClauseCompanies + '
    ORDER BY c.Id
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    ';

    -- Execute the dynamically built SQL
    EXEC sp_executesql @SQL, 
         N'@PageNumber INT, @PageSize INT, @CompanySize dbo.ContactFilterType READONLY, @LeadDivision dbo.ContactFilterType READONLY, @SeniorityLevel dbo.ContactFilterType READONLY, @EmailScore dbo.ContactFilterType READONLY, @CompanyIndustry dbo.ContactFilterType READONLY, @RevenueRange dbo.ContactFilterType READONLY',
         @PageNumber = @PageNumber, 
         @PageSize = @PageSize,
         @CompanySize = @CompanySize,
         @LeadDivision = @LeadDivision,
         @SeniorityLevel = @SeniorityLevel,
         @EmailScore = @EmailScore,
         @CompanyIndustry = @CompanyIndustry,
         @RevenueRange = @RevenueRange;
END;
