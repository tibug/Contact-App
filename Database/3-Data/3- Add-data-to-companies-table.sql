USE DBContacts;
GO

BEGIN TRANSACTION;

BEGIN TRY

-- Insert data into Companies
INSERT INTO Companies (
    UniqueId,
    CompanyName,
    CompanyWebsite,
    CompanyPhoneNumbers,
    CompanyLocationText,
    CompanyTypeID,
    CompanyFunctionID,
    CompanySectorID,
    CompanyIndustryID,
    CompanyFoundedAt,
    CompanyFundingRange,
    RevenueRange,
    EBITDARange,
    CompanyFacebookPage,
    CompanyTwitterPage,
    CompanyLinkedinPage,
    CompanySICCode,
    CompanyNAICSCode,
    CompanyDescription,
    CompanySizeID,
    CompanyProfileImageUrl,
    CompanyProductsServices,
    keywords
)
SELECT 
    c.company_id,
    c.company_name,
    c.company_website,
    c.company_phone_numbers,
    c.company_location_text,
    ct.Id AS CompanyTypeID,
    cf.Id AS CompanyFunctionID,
    cs.Id AS CompanySectorID,
    ci.Id AS CompanyIndustryID,
    c.company_founded_at,
    c.company_funding_range,
    c.revenue_range,
    c.ebitda_range,
    c.company_facebook_page,
    c.Company_Twitter_Page,
    c.Company_Linkedin_Page,
    c.Company_SIC_Code,
    c.Company_NAICS_Code,
    c.company_description,
    csz.Id AS CompanySizeID,
    c.company_profile_image_url,
    c.company_products_services,
    c.keywords
FROM tblContacts AS c
LEFT JOIN CompanyTypes ct ON ct.CompanyType = c.company_type
LEFT JOIN CompanyFunctions cf ON cf.CompanyFunction = c.company_function
LEFT JOIN CompanySectors cs ON cs.CompanySector = c.company_sector
LEFT JOIN CompanyIndustries ci ON ci.CompanyIndustry = c.company_industry
LEFT JOIN CompanySizes csz ON csz.CompanySize = c.company_size
WHERE NOT EXISTS (
    SELECT 1
    FROM Companies AS comp
    WHERE LTRIM(RTRIM(comp.CompanyName)) = LTRIM(RTRIM(c.company_name))
    AND LTRIM(RTRIM(comp.UniqueId)) = LTRIM(RTRIM(c.company_id))  -- Ensure to use c.company_id
);

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