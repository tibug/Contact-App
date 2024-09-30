
using ContactApp.Models.Dtos;
using CsvHelper;
using System.Globalization;

namespace ContactApp.Web.Controllers;

[Authorize]
public class ContactFilterController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly INotyfService _notyfService;
    private readonly IContactRepository _contactRepository;
    private readonly ILookupRepository _lookupRepository;
    private readonly ILogger<ContactFilterController> _logger;

    public ContactFilterController(ApplicationDbContext db, INotyfService notyfService,
        ILogger<ContactFilterController> logger,
        IContactRepository contactRepository, ILookupRepository lookupRepository)
    {
        _db = db;
        _notyfService = notyfService;
        _logger = logger;
        _contactRepository = contactRepository;
        _lookupRepository = lookupRepository;
    }
    private async Task<List<JobCategory>> GetJobCategories()
    {
        var leadTitles = await _lookupRepository.GetLeadDivisionsAsync(); // Ensure this method fetches job titles

        var jobCategories = new List<JobCategory>
{
    new JobCategory
    {
        CategoryName = "C-Suite",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading1")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Finance",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading2")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Legal",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading3")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Information Security",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading4")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Operations",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading5")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Product Development",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading6")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Data Science & Analytics",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading7")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Technology & Infrastructure",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading8")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Customer Success & Support",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading9")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "HR",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading10")
            .ToList()
    },
    new JobCategory
    {
        CategoryName = "Marketing",
        Titles = leadTitles
            .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading11")
            .ToList()
    },
};


        return jobCategories;
    }

    private async Task<List<IndustryCategory>> GetCompanyIndustry()
    {
        var leadTitles = await _lookupRepository.GetCompanyIndustriesAsync(); // Ensure this method fetches job titles

        var industryCategories = new List<IndustryCategory>
        {
            new IndustryCategory
            {
                CategoryName = "Agriculture",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading1")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Business Services",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading2")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Construction",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading3")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Consumer Services",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading4")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Education",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading5")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Energy, Utilities & Waste",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading6")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Finance",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading7")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Government",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading8")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Healthcare Services",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading9")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Conglomerates",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading24")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Hospitality",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading10")
                    .ToList()
            },
                        new IndustryCategory
            {
                CategoryName = "Hospitals",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading11")
                    .ToList()
            },
                                    new IndustryCategory
            {
                CategoryName = "Insurance",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading12")
                    .ToList()
            },
                                                new IndustryCategory
            {
                CategoryName = "Law Firms",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading13")
                    .ToList()
            },
                                                            new IndustryCategory
            {
                CategoryName = "Manufacturing",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading14")
                    .ToList()
            },
                                                                        new IndustryCategory
            {
                CategoryName = "Media & Internet",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading15")
                    .ToList()
            },
                                                                                    new IndustryCategory
            {
                CategoryName = "Minerals & Mining",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading16")
                    .ToList()
            },
                                                                                                new IndustryCategory
            {
                CategoryName = "Organizations",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading17")
                    .ToList()
            },

                                                                                                new IndustryCategory
            {
                CategoryName = "Real Estate",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading18")
                    .ToList()
            },

                                                                                                new IndustryCategory
            {
                CategoryName = "Retail",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading19")
                    .ToList()
            },

                                                                                                new IndustryCategory
            {
                CategoryName = "Software",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading20")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Telecommunications",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading21")
                    .ToList()
            }
            ,
            new IndustryCategory
            {
                CategoryName = "Transportation",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading22")
                    .ToList()
            },
            new IndustryCategory
            {
                CategoryName = "Additional Subcategories",
                Industries = leadTitles
                    .Where(t => !string.IsNullOrEmpty(t.Heading) && t.Heading.ToLower() == "heading23")
                    .ToList()
            }
        };


        return industryCategories;
    }

    public async Task<IActionResult> Index()
    {

        string email = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase)).Value;
        var result = await _db.tblUsers.Where(x => x.Email == email).FirstOrDefaultAsync();

        if (result == null)
        {
            return RedirectToAction("Index", "Login");
        }

        try
        {
            var companySizes = await _lookupRepository.GetCompanySizesAsync();
            var revenueSizes = await _lookupRepository.GetRevenueSizesAsync();
            var accuracyScores = await _lookupRepository.GetAccuracyScoresAsync();
            var levels = await _lookupRepository.GetSeniorityLevelsAsync();
            var industries = await GetCompanyIndustry(); //await _lookupRepository.GetCompanyIndustriesAsync();
            var jobCategories = await GetJobCategories();// await _lookupRepository.GetLeadTitlesAsync();

            ViewBag.CompanySizes = companySizes;
            ViewBag.RevenueSizes = revenueSizes;
            ViewBag.AccuracyScores = accuracyScores;
            ViewBag.Levels = levels;
            ViewBag.Industries = industries;
            ViewBag.JobCategories = jobCategories;

            var companyName = string.Empty;
            int? seniorityLevelId = 0;
            if (TempData["CompanyName"] != null && TempData["SeniorityLevelId"] != null)
            {
                companyName = TempData["CompanyName"].ToString();
                seniorityLevelId = (int)TempData["SeniorityLevelId"];
            }
            ViewBag.SelectedSeniorityLevel = seniorityLevelId ?? 0;
            ViewBag.CompanyName = companyName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while loading data.");
        }

        ViewBag.totalcount = 0;


        try
        {

            string email_id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase)).Value;
            var uderdata2 = await _db.tblUsers.Where(x => x.Email == email_id).FirstOrDefaultAsync();
            int searchlimit2 = Convert.ToInt32(uderdata2.searchlimit);
            int AllSearchLimit3 = _db.tblSearchlimit.Where(option => option.SearchBy == uderdata2.Id && option.SearchLimitDate.Date == DateTime.Today).Select(option => option.SearchLimitCount).Sum();

            if (AllSearchLimit3 >= searchlimit2 + 1)
            {
                string resetTime = DateTime.Now.AddDays(1).ToString("MMM dd hh:mm tt");
                _notyfService.Error($"You’ve reached your daily export. Please Upgrade your plan. Your limit will reset on {resetTime}");

            }
        }
        catch (Exception e) { }


        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] CustomSearchContact? request, [FromQuery] int? ispageload = 1)
    {
        // Null check for request object
        if (request == null)
        {
            return BadRequest("Invalid request data.");
        }
        try
        {
            if (request.Draw >= 50)
                request.Draw = 50;

            // Ensure the user is authenticated
            if (!(User.Identity is ClaimsIdentity identity))
            {
                return Unauthorized();
            }

            // Extract email from user claims
            string email = identity.Claims
                .FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase))?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            // Fetch user data
            var userData = await _db.tblUsers.FirstOrDefaultAsync(x => x.Email == email);
            if (userData == null)
            {
                _logger.LogWarning($"User not found for email: {email}");
                return RedirectToAction("Index", "Login");
            }

            // Initialize user-related variables
            int searchLimit = Convert.ToInt32(userData.searchlimit);
            bool isActive = userData.isActive;
            int searchlimitID = Convert.ToInt32(userData.Id);

            // Check the user's daily search limit
            int allSearchLimitToday = await _db.tblSearchlimit
                .Where(option => option.SearchBy == searchlimitID && option.SearchLimitDate.Date == DateTime.Today)
                .SumAsync(option => option.SearchLimitCount);

            // Check if the search limit has been reached
            if (allSearchLimitToday > searchLimit)
            {
                Response.Headers.Add("resetTime", Convert.ToString(DateTime.Today));
                _logger.LogInformation($"Search limit reached for user {email}. Limit: {searchLimit}, Current: {allSearchLimitToday}");
                return Json(new { message = "Search limit reached" });
            }

            // Fetch contacts if the search limit hasn't been reached
            (int recordsTotal, int recordsFiltered, IEnumerable<ContactDto> contacts) = await _contactRepository.GetContactsAsync(request);

            // Update search limit if necessary
            if (allSearchLimitToday < searchLimit + 1)
            {
                _db.tblSearchlimit.Add(new SearchLimit
                {
                    SearchLimitCount = 1,
                    SearchLimitDate = DateTime.Now,
                    SearchBy = searchlimitID,
                    Email = userData.Email,
                    password = userData.password
                });
                await _db.SaveChangesAsync();
            }

            // Calculate remaining search limit
            int remainingSearchLimit = Math.Max(searchLimit - allSearchLimitToday, 0);

            // Manage cookies for remaining search limits
            string searchLimitKey = email.Replace("@", "_").Replace(".", "_") + "rem";
            Response.Cookies.Delete(searchLimitKey);

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append(searchLimitKey, remainingSearchLimit.ToString(), cookieOptions);

            // Set response headers for remaining search limit
            Response.Headers.Add("remval", remainingSearchLimit.ToString());

            // If the search limit is exhausted, return null data
            if (remainingSearchLimit == 0 && allSearchLimitToday >= searchLimit + 1)
            {
                _logger.LogInformation($"Search limit exhausted for user {email}.");
                return Json(null);
            }

            // Return the contacts and pagination information
            var response = new
            {
                draw = request.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = contacts
            };

            return Json(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing GetAll.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    [Route("ContactFilter/ExportContactsToCsv")]
    public async Task<IActionResult> ExportContactsToCSV([FromBody] CustomSearchContact? searchdata)
    {
        try
        {
            string email = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase)).Value;
            var uderdata = await _db.tblUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (uderdata == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int remval = 0;
            var key2 = email.Replace("@", "_").Replace(".", "_") + "rem";
            int exportlimit = Convert.ToInt32(searchdata.PerExportLimit);
            int todayexportlimit = Convert.ToInt32(uderdata.ExportTodayLimit);

            int AllExports = _db.tblUserExport.Where(option => option.ExportBy == uderdata.Id && option.ExportDate.Date == DateTime.Today).Select(option => option.ExportCount).Sum();

            if (exportlimit > uderdata.PerExportLimit)
            {
                Response.Headers.Add("perlimitavailable", "N");
                Response.Headers.Add("limitavailable", "");
                return Json(null);
            }

            if ((todayexportlimit - AllExports) > 0 && todayexportlimit >= exportlimit)
            {
                remval = (todayexportlimit - AllExports);
            }
            else
            {
                remval = 0;
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Delete(key2);
            if (remval == 0 || exportlimit > remval)
            {
                Response.Headers.Add("limitavailable", "N");
                Response.Headers.Add("perlimitavailable", "");

                return Json(null);
            }

            IEnumerable<ExportContactsToCsv> result = null;
            if (remval <= todayexportlimit)
            {
                result = await _contactRepository.ExportContactsToCsvAsync(searchdata, exportlimit);
                if (remval > 0)
                {
                    remval = remval - exportlimit;
                }
                else
                {
                    remval = todayexportlimit - exportlimit;
                }

                if (remval <= 0)
                {
                    remval = 0;
                }
            }
            else if (remval < exportlimit && remval > 0)
            {
                result = await _contactRepository.ExportContactsToCsvAsync(searchdata, remval);
                remval = 0;
            }

            // Stream large CSV data as a file
            var csvFileStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(csvFileStream, leaveOpen: true))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                await csvWriter.WriteRecordsAsync(result);
                await streamWriter.FlushAsync();
            }

            // Reset stream position for reading
            csvFileStream.Position = 0;

            Response.Headers.Add("remval", remval.ToString());
            Response.Headers.Add("limitavailable", "Y");
            Response.Headers.Add("perlimitavailable", "Y");

            Response.Cookies.Append(key2, remval.ToString(), option);

            _db.tblUserExport.Add(
                new UserExport
                {
                    Id = 0,
                    ExportCount = exportlimit,
                    ExportDate = DateTime.Now,
                    ExportBy = uderdata.Id
                }
                );
            _db.SaveChanges();
            int AllExports1 = _db.tblUserExport.Where(option => option.ExportBy == uderdata.Id && option.ExportDate.Date == DateTime.Today).Select(option => option.ExportCount).Sum();
            ViewBag.rem = (exportlimit - AllExports1).ToString();
            return File(csvFileStream, "text/csv", "exported_data.csv");
        }
        catch (FormatException ex)
        {
            return BadRequest(new { Message = "Invalid format provided for numeric fields.", Details = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving data from the database.", Details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

}
