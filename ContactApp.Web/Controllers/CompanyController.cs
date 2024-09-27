namespace ContactApp.Web.Controllers;

[Authorize]
public class CompanyController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly ICompanyRepository _companyRepository;
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ApplicationDbContext db, ICompanyRepository companyRepository, ILogger<CompanyController> logger)
    {
        _db = db;
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Route("company/{companyId:int}")]
    public async Task<IActionResult> CompanyProfile(int companyId)
    {
        string email = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase)).Value;
        var result = await _db.tblUsers.Where(x => x.Email == email).FirstOrDefaultAsync();

        if (result == null)
        {
            return RedirectToAction("Index", "Login");
        }

        try
        {
            if (companyId <= 0)
            {
                _logger.LogWarning("Invalid companyId: {companyId}", companyId);
                return BadRequest("Invalid company ID.");
            }

            var companyProfile = await _companyRepository.GetCompanyProfileByCompanyIdAsync(companyId);

            if (companyProfile == null)
            {
                _logger.LogWarning("Company profile not found for companyId: {companyId}", companyId);
                return NotFound($"Company profile with ID {companyId} not found.");
            }

            return View(companyProfile);
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "An error occurred while fetching the company profile for companyId: {companyId}", companyId);
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}
