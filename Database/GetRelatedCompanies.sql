ALTER PROCEDURE [dbo].[GetRelatedCompanies]
    @CompanyId INT,
    @ProductServices dbo.StringList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    -- Use a temporary table to store the valid Company IDs
    CREATE TABLE #ValidCompanies (CompanyId INT PRIMARY KEY);

    -- Insert into the temporary table based on matching products/services
    INSERT INTO #ValidCompanies (CompanyId)
    SELECT DISTINCT TOP 50 c.Id
    FROM Companies c
    WHERE c.Id <> @CompanyId
      AND EXISTS (
          SELECT 1
          FROM @ProductServices ps
          WHERE c.CompanyProductsServices LIKE '%' + LTRIM(RTRIM(ps.Value)) + '%' -- Avoid leading %
      );

    -- Check if there are valid companies
    IF EXISTS (SELECT 1 FROM #ValidCompanies)
    BEGIN
        -- Select a random 4 from the valid companies using RAND() instead of CHECKSUM(NEWID())
        SELECT TOP 4 
            c.Id AS CompanyId, 
            c.CompanyName, 
	    CASE 
            WHEN c.CompanyProfileImageUrl IS NULL OR c.CompanyProfileImageUrl = '' THEN NULL 
            WHEN LEFT(c.CompanyProfileImageUrl, 4) <> 'http' THEN 'https://' + c.CompanyProfileImageUrl 
            ELSE c.CompanyProfileImageUrl 
        END AS CompanyProfileImageUrl
        FROM Companies c
        INNER JOIN #ValidCompanies vc ON c.Id = vc.CompanyId
        ORDER BY RAND(); -- Using RAND() for randomization
    END

    DROP TABLE #ValidCompanies;  -- Clean up temporary table
END;
