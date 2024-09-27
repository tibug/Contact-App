//using ContactApp.DataAccess.IRepository;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using ContactApp.Models;


//namespace ContactApp.Web.Controllers
//{
//    [Authorize(Roles = "admin")]
//    public class ContractController : Controller
//    {
//        private readonly IContact _contact;
//        private readonly string _connectionString;
//        bool IsSuccess = false;


//        public ContractController(IContact contact, IConfiguration _configuration)
//        {
//            _contact = contact;
//            _connectionString = _configuration.GetConnectionString("Conn");
          
           
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Index(Contact contactData)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _contact.Save(contactData);
//                if (result.IsSuccess)
//                {
//                    TempData["success"] = "Successfully Saved Record";
                 
//                    ModelState.Clear();
//                }
//                else
//                {
//                    TempData["error"] = result.Message;
//                }
//            }
//            return View();
//        }
//        public IActionResult Upload()
//        {
//            return View();
//        }

//        //[HttpPost]
//        //[RequestSizeLimit(100_000_000)]
//        //public async Task<IActionResult> Upload(IFormFile fileInput)
//        //{
//        //    if (fileInput == null || fileInput.Length == 0)
//        //    {
//        //        return BadRequest("File not selected or empty.");
//        //    }

//        //    try
//        //    {
//        //        var contacts = new List<Models.Contact>();

//        //        using (var streamReader = new StreamReader(fileInput.OpenReadStream()))
//        //        {
//        //            using (var csvReader = new CsvHelper.CsvReader(streamReader, CultureInfo.InvariantCulture)) // Fully qualify CsvReader
//        //            {

//        //                csvReader.Read(); // Skip the header row
//        //                csvReader.ReadHeader(); // Use header row for mapping

//        //                try
//        //                {

//        //                    while (csvReader.Read())
//        //                    {
//        //                        var contact = csvReader.GetRecord<Models.Contact>();
//        //                        contacts.Add(contact);
//        //                    }
//        //                }
//        //                catch (Exception ex) { }
//        //            }
//        //        }

//        //        var result = await _contact.SaveRange(contacts);

//        //        if (result.IsSuccess)
//        //        {
//        //            return Ok(new { success = true, message = result.Message });
//        //        }

//        //        return Ok(new { success = false, message = result.Message });
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return BadRequest(ex.Message);
//        //    }
//        //}




//        [HttpPost]
//        public async Task<IActionResult> Upload(IFormFile fileInput)
//        {
           
//            if (fileInput == null || fileInput.Length == 0)
//            {

//                 return BadRequest("File not selected or empty.");
//            }

//            else if (fileInput != null && fileInput.Length > 0)
//            {
//                try
//                {
//                    using (var stream = new StreamReader(fileInput.OpenReadStream()))
//                    {
//                        var jsonData = await stream.ReadToEndAsync();
//                        var contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonData);

//                        SaveContactsToDatabase(contacts);
//                        IsSuccess = true;

//                    }

//                    if (IsSuccess == true)
//                    {
//                        return Ok(new { success = true, message = "Successfully Saved Record" });
//                    }

//                    return Ok(new { success = false, message = "Please upload a valid JSON file." });

//                }
//                catch (Exception ex) { return BadRequest(ex.Message); }
//            }
//            else {  }

          
//            return View();
//        }

//        private void SaveContactsToDatabase(List<Contact> contacts)
//        {
//            try {
//                using (SqlConnection connection = new SqlConnection(_connectionString))
//                {
//                    connection.Open();
//                    foreach (var contact in contacts)
//                    {
//                        string query = "INSERT INTO tblContacts (id,company_id,name,email,email_score,phone,work_phone,lead_location,lead_division,lead_titles,decision_making_power,seniority_level,linkedin_url,skills,past_companies,company_name,company_size,company_website,company_phone_numbers,company_location_text,company_type,company_function,company_sector,company_industry,company_founded_at,company_funding_range,revenue_range,ebitda_range,company_facebook_page,company_twitter_page,company_linkedin_page,company_sic_code,company_naics_code,company_description,company_size_key,company_profile_image_url)" +
//                                                        "VALUES (@id,@company_id,@name, @email,@email_score,@phone,@work_phone,@lead_location,@lead_division,@lead_titles,@decision_making_power,@seniority_level,@linkedin_url,@skills,@past_companies,@company_name,@company_size,@company_website,@company_phone_numbers,@company_location_text,@company_type,@company_function,@company_sector,@company_industry,@company_founded_at,@company_funding_range,@revenue_range,@ebitda_range,@company_facebook_page,@company_twitter_page,@company_linkedin_page,@company_sic_code,@company_naics_code,@company_description,@company_size_key,@company_profile_image_url)";
//                        using (SqlCommand command = new SqlCommand(query, connection))
//                        {
//                            command.Parameters.AddWithValue("@id", contact.id);
//                            command.Parameters.AddWithValue("@company_id", contact.company_id);
//                            command.Parameters.AddWithValue("@name", contact.name);
//                            command.Parameters.AddWithValue("@email", contact.email);
//                            command.Parameters.AddWithValue("@email_score", contact.email_score);
//                            command.Parameters.AddWithValue("@phone", contact.phone);
//                            command.Parameters.AddWithValue("@work_phone", contact.work_phone);
//                            command.Parameters.AddWithValue("@lead_location", contact.lead_location);
//                            command.Parameters.AddWithValue("@lead_division", contact.lead_division);
//                            command.Parameters.AddWithValue("@lead_titles", contact.lead_titles);
//                            command.Parameters.AddWithValue("@decision_making_power", contact.decision_making_power);
//                            command.Parameters.AddWithValue("@seniority_level", contact.seniority_level);
//                            command.Parameters.AddWithValue("@linkedin_url", contact.linkedin_url);
//                            command.Parameters.AddWithValue("@skills", contact.skills);
//                            command.Parameters.AddWithValue("@past_companies", contact.past_companies);
//                            command.Parameters.AddWithValue("@company_name", contact.company_name);
//                            command.Parameters.AddWithValue("@company_size", contact.company_size);
//                            command.Parameters.AddWithValue("@company_website", contact.company_website);
//                            command.Parameters.AddWithValue("@company_phone_numbers", contact.company_phone_numbers);
//                            command.Parameters.AddWithValue("@company_location_text", contact.company_location_text);
//                            command.Parameters.AddWithValue("@company_type", contact.company_type);
//                            command.Parameters.AddWithValue("@company_function", contact.company_function);
//                            command.Parameters.AddWithValue("@company_sector", contact.company_sector);
//                            command.Parameters.AddWithValue("@company_industry", contact.company_industry);
//                            command.Parameters.AddWithValue("@company_founded_at", contact.company_founded_at);
//                            command.Parameters.AddWithValue("@company_funding_range", contact.company_funding_range);
//                            command.Parameters.AddWithValue("@revenue_range", contact.revenue_range);
//                            command.Parameters.AddWithValue("@ebitda_range", contact.ebitda_range);
//                            command.Parameters.AddWithValue("@company_facebook_page", contact.company_facebook_page);
//                            command.Parameters.AddWithValue("@company_twitter_page", contact.company_twitter_page);
//                            command.Parameters.AddWithValue("@company_linkedin_page", contact.company_linkedin_page);
//                            command.Parameters.AddWithValue("@company_sic_code", contact.company_sic_code);
//                            command.Parameters.AddWithValue("@company_naics_code", contact.company_naics_code);
//                            command.Parameters.AddWithValue("@company_description", contact.company_description);
//                            command.Parameters.AddWithValue("@company_size_key", contact.company_size_key);
//                            command.Parameters.AddWithValue("@company_profile_image_url", contact.company_profile_image_url);


//                            command.ExecuteNonQuery();
//                        }
//                    }
//                }
               
//            }
//            catch (Exception ex) { }

          

//        }
//    }
//}







////[HttpPost]  
////public async Task<IActionResult> Upload(IFormFile fileInput)
////{
////    if (fileInput == null || fileInput.Length == 0)
////    {
////        return BadRequest("File not selected or empty.");
////    }

////    try
////    {
////        var contacts = new List<Models.Contact>();

////        using (var reader = new StreamReader(fileInput.OpenReadStream()))
////        {
////            // Skip the first line (header)
////            await reader.ReadLineAsync();

////            while (!reader.EndOfStream)
////            {
////                var line = await reader.ReadLineAsync();
////                var values = line.Split(',');

////                var contact = new Models.Contact
////                {

////                    name = values[1],
////                    email = values[2],
////                    email_score = values[3],
////                    phone = values[4],
////                    work_phone = values[5],
////                    lead_location = values[6],
////                    lead_divison = values[7],
////                    lead_titles = values[8],
////                    decision_making_power = values[9],
////                    seniority_level = values[10],
////                    linkedin_url = values[11],
////                    skills = values[12],
////                    past_companies = values[13],
////                    company_name = values[14],
////                    company_size = values[15],
////                    company_website = values[16],
////                    company_phone_numbers = values[17],
////                    company_location_text = values[18],
////                    company_type = values[19],
////                    company_function = values[20],
////                    company_sector = values[21],
////                    company_industry = values[22],
////                    company_founded_at = values[23],
////                    company_funding_range = values[24],
////                    revenue_range = values[25],
////                    ebitda_range = values[26],
////                    company_facebook_page = values[27],
////                    company_twitter_page = values[28],
////                    company_linkedin_page = values[29],
////                    company_sic_code = values[30],
////                    company_naics_code = values[31],
////                    company_description = values[32],
////                    company_size_key = values[33],
////                    company_profile_image_url = values[34]

////                };

////                contacts.Add(contact);
////            }
////        }

////        var result = await _contact.SaveRange(contacts);

////        if (result.IsSuccess)
////        {
////            return Ok(new { success = true, message = result.Message });
////        }

////        return Ok(new { success = false, message = result.Message });
////    }
////    catch (Exception ex)
////    {
////        return BadRequest(ex.Message);
////    }
////}




////}

