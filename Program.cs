using System;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Numerics;
using System.Xml.Linq;

namespace JsonToSqlImporter
{
    class Program
    {
        // Define connection string to your SQL Server database
        //private static string connectionString = "Data Source=LAPTOP-77TM661O;Initial Catalog=DBContacts;Persist Security Info=True;Integrated Security=true;TrustServerCertificate=True;ConnectRetryCount=5;ConnectRetryInterval=10;";

        private static string connectionString = "Data Source=LAPTOP-77TM661O;Initial Catalog=DBContacts;Persist Security Info=True;User ID=sa;Password=123456;TrustServerCertificate=True;ConnectRetryCount=5;ConnectRetryInterval=10;Max Pool Size=200;";

        static async Task Main(string[] args)
        {
            try
            {
                string directoryPath = @"E:\Projects\Contacts\Testing-Data\Deve";
                string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

                // Create a list to hold the tasks
                var tasks = new List<Task>();

                foreach (var filePath in jsonFiles)
                {
                    tasks.Add(Task.Run(() => ProcessFile(filePath)));
                }

                // Wait for all tasks to complete
                await Task.WhenAll(tasks);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private static void ProcessFile(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);

                // Deserialize JSON to a list of Contact objects
                List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonData);

                // Bulk insert contacts into the database
                BulkInsertContacts(contacts);

                Console.WriteLine("Data imported successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        static void BulkInsertContacts(List<Contact> contacts)
        {
            DataTable contactTable = CreateContactDataTable();

            foreach (var contact in contacts)
            {
                DataRow row = contactTable.NewRow();
                row["id"] = contact.id ?? (object)DBNull.Value;
                row["company_id"] = contact.company_id ?? (object)DBNull.Value;
                row["name"] = contact.name ?? (object)DBNull.Value;
                row["email"] = contact.email ?? (object)DBNull.Value;
                row["email_score"] = contact.email_score ?? (object)DBNull.Value;
                row["phone"] = contact.phone ?? (object)DBNull.Value;
                row["work_phone"] = contact.work_phone ?? (object)DBNull.Value;
                row["lead_location"] = contact.lead_location ?? (object)DBNull.Value;
                row["lead_division"] = contact.lead_division ?? (object)DBNull.Value;
                row["lead_titles"] = contact.lead_titles ?? (object)DBNull.Value;
                row["decision_making_power"] = contact.decision_making_power ?? (object)DBNull.Value;
                row["seniority_level"] = contact.seniority_level ?? (object)DBNull.Value;
                row["linkedin_url"] = contact.linkedin_url ?? (object)DBNull.Value;
                row["skills"] = contact.skills ?? (object)DBNull.Value;
                row["past_companies"] = contact.past_companies ?? (object)DBNull.Value;
                row["company_name"] = contact.company_name ?? (object)DBNull.Value;
                row["company_size"] = contact.company_size ?? (object)DBNull.Value;
                row["company_website"] = contact.company_website ?? (object)DBNull.Value;
                row["company_phone_numbers"] = contact.company_phone_numbers ?? (object)DBNull.Value;
                row["company_location_text"] = contact.company_location_text ?? (object)DBNull.Value;
                row["company_type"] = contact.company_type ?? (object)DBNull.Value;
                row["company_function"] = contact.company_function ?? (object)DBNull.Value;
                row["company_sector"] = contact.company_sector ?? (object)DBNull.Value;
                row["company_industry"] = contact.company_industry ?? (object)DBNull.Value;
                row["company_founded_at"] = contact.company_founded_at ?? (object)DBNull.Value;
                row["company_funding_range"] = contact.company_funding_range ?? (object)DBNull.Value;
                row["revenue_range"] = contact.revenue_range ?? (object)DBNull.Value;
                row["ebitda_range"] = contact.ebitda_range ?? (object)DBNull.Value;
                row["company_facebook_page"] = contact.company_facebook_page ?? (object)DBNull.Value;
                row["company_twitter_page"] = contact.company_twitter_page ?? (object)DBNull.Value;
                row["company_linkedin_page"] = contact.company_linkedin_page ?? (object)DBNull.Value;
                row["company_sic_code"] = contact.company_sic_code ?? (object)DBNull.Value;
                row["company_naics_code"] = contact.company_naics_code ?? (object)DBNull.Value;
                row["company_description"] = contact.company_description ?? (object)DBNull.Value;
                row["company_size_key"] = contact.company_size_key;
                row["company_profile_image_url"] = contact.company_profile_image_url ?? (object)DBNull.Value;
                row["company_products_services"] = contact.company_products_services ?? (object)DBNull.Value;
                row["keywords"] = contact.keywords ?? (object)DBNull.Value;

                contactTable.Rows.Add(row);
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "[dbo].[tblContacts]";

                    // Increase the timeout to 10 minutes (600 seconds)
                    bulkCopy.BulkCopyTimeout = 600;

                    // Map the columns
                    bulkCopy.ColumnMappings.Add("id", "id");
                    bulkCopy.ColumnMappings.Add("company_id", "company_id");
                    bulkCopy.ColumnMappings.Add("name", "name");
                    bulkCopy.ColumnMappings.Add("email", "email");
                    bulkCopy.ColumnMappings.Add("email_score", "email_score");
                    bulkCopy.ColumnMappings.Add("phone", "phone");
                    bulkCopy.ColumnMappings.Add("work_phone", "work_phone");
                    bulkCopy.ColumnMappings.Add("lead_location", "lead_location");
                    bulkCopy.ColumnMappings.Add("lead_division", "lead_division");
                    bulkCopy.ColumnMappings.Add("lead_titles", "lead_titles");
                    bulkCopy.ColumnMappings.Add("decision_making_power", "decision_making_power");
                    bulkCopy.ColumnMappings.Add("seniority_level", "seniority_level");
                    bulkCopy.ColumnMappings.Add("linkedin_url", "linkedin_url");
                    bulkCopy.ColumnMappings.Add("skills", "skills");
                    bulkCopy.ColumnMappings.Add("past_companies", "past_companies");
                    bulkCopy.ColumnMappings.Add("company_name", "company_name");
                    bulkCopy.ColumnMappings.Add("company_size", "company_size");
                    bulkCopy.ColumnMappings.Add("company_website", "company_website");
                    bulkCopy.ColumnMappings.Add("company_phone_numbers", "company_phone_numbers");
                    bulkCopy.ColumnMappings.Add("company_location_text", "company_location_text");
                    bulkCopy.ColumnMappings.Add("company_type", "company_type");
                    bulkCopy.ColumnMappings.Add("company_function", "company_function");
                    bulkCopy.ColumnMappings.Add("company_sector", "company_sector");
                    bulkCopy.ColumnMappings.Add("company_industry", "company_industry");
                    bulkCopy.ColumnMappings.Add("company_founded_at", "company_founded_at");
                    bulkCopy.ColumnMappings.Add("company_funding_range", "company_funding_range");
                    bulkCopy.ColumnMappings.Add("revenue_range", "revenue_range");
                    bulkCopy.ColumnMappings.Add("ebitda_range", "ebitda_range");
                    bulkCopy.ColumnMappings.Add("company_facebook_page", "company_facebook_page");
                    bulkCopy.ColumnMappings.Add("company_twitter_page", "company_twitter_page");
                    bulkCopy.ColumnMappings.Add("company_linkedin_page", "company_linkedin_page");
                    bulkCopy.ColumnMappings.Add("company_sic_code", "company_sic_code");
                    bulkCopy.ColumnMappings.Add("company_naics_code", "company_naics_code");
                    bulkCopy.ColumnMappings.Add("company_description", "company_description");
                    bulkCopy.ColumnMappings.Add("company_size_key", "company_size_key");
                    bulkCopy.ColumnMappings.Add("company_profile_image_url", "company_profile_image_url");
                    bulkCopy.ColumnMappings.Add("company_products_services", "company_products_services");
                    bulkCopy.ColumnMappings.Add("keywords", "keywords");

                    bulkCopy.WriteToServer(contactTable);
                }
            }
        }
        // Create DataTable schema for Contact
        static DataTable CreateContactDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("company_id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("email", typeof(string));
            table.Columns.Add("email_score", typeof(int));
            table.Columns.Add("phone", typeof(string));
            table.Columns.Add("work_phone", typeof(string));
            table.Columns.Add("lead_location", typeof(string));
            table.Columns.Add("lead_division", typeof(string));
            table.Columns.Add("lead_titles", typeof(string));
            table.Columns.Add("decision_making_power", typeof(string));
            table.Columns.Add("seniority_level", typeof(string));
            table.Columns.Add("linkedin_url", typeof(string));
            table.Columns.Add("skills", typeof(string));
            table.Columns.Add("past_companies", typeof(string));
            table.Columns.Add("company_name", typeof(string));
            table.Columns.Add("company_size", typeof(string));
            table.Columns.Add("company_website", typeof(string));
            table.Columns.Add("company_phone_numbers", typeof(string));
            table.Columns.Add("company_location_text", typeof(string));
            table.Columns.Add("company_type", typeof(string));
            table.Columns.Add("company_function", typeof(string));
            table.Columns.Add("company_sector", typeof(string));
            table.Columns.Add("company_industry", typeof(string));
            table.Columns.Add("company_founded_at", typeof(string));
            table.Columns.Add("company_funding_range", typeof(string));
            table.Columns.Add("revenue_range", typeof(string));
            table.Columns.Add("ebitda_range", typeof(string));
            table.Columns.Add("company_facebook_page", typeof(string));
            table.Columns.Add("company_twitter_page", typeof(string));
            table.Columns.Add("company_linkedin_page", typeof(string));
            table.Columns.Add("company_sic_code", typeof(string));
            table.Columns.Add("company_naics_code", typeof(string));
            table.Columns.Add("company_description", typeof(string));
            table.Columns.Add("company_size_key", typeof(int));
            table.Columns.Add("company_profile_image_url", typeof(string));
            table.Columns.Add("company_products_services", typeof(string));
            table.Columns.Add("keywords", typeof(string));

            return table;
        }
    }

    public class Contact
    {
        public string id { get; set; }
        public string company_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int? email_score { get; set; }
        public string phone { get; set; }
        public string work_phone { get; set; }
        public string lead_location { get; set; }
        public string lead_division { get; set; }
        public string lead_titles { get; set; }
        public string decision_making_power { get; set; }
        public string seniority_level { get; set; }
        public string linkedin_url { get; set; }
        public string skills { get; set; }
        public string past_companies { get; set; }
        public string company_name { get; set; }
        public string company_size { get; set; }
        public string company_website { get; set; }
        public string company_phone_numbers { get; set; }
        public string company_location_text { get; set; }
        public string company_type { get; set; }
        public string company_function { get; set; }
        public string company_sector { get; set; }
        public string company_industry { get; set; }
        public string company_founded_at { get; set; }
        public string company_funding_range { get; set; }
        public string revenue_range { get; set; }
        public string ebitda_range { get; set; }
        public string company_facebook_page { get; set; }
        public string company_twitter_page { get; set; }
        public string company_linkedin_page { get; set; }
        public string company_sic_code { get; set; }
        public string company_naics_code { get; set; }
        public string company_description { get; set; }
        public int company_size_key { get; set; }
        public string company_profile_image_url { get; set; }
        public string company_products_services { get; set; }
        public string keywords { get; set; } 
    }
}


//USE[DBContacts]
//GO

///****** Object:  Table [dbo].[tblContacts]    Script Date: 9/25/2024 12:20:18 AM ******/
//SET ANSI_NULLS ON
//GO

//SET QUOTED_IDENTIFIER ON
//GO

//CREATE TABLE [dbo].[tblContacts](
//    [Ids][int] IDENTITY(1, 1) NOT NULL,
//    [id][nvarchar](max) NULL,
//    [company_id][nvarchar](max) NULL,
//    [name][nvarchar](max) NULL,
//    [email][nvarchar](max) NULL,
//    [email_score][nvarchar](max) NULL,
//    [phone][nvarchar](max) NULL,
//    [work_phone][nvarchar](max) NULL,
//    [lead_location][nvarchar](max) NULL,
//    [lead_division][nvarchar](max) NULL,
//    [lead_titles][nvarchar](max) NULL,
//    [decision_making_power][nvarchar](max) NULL,
//    [seniority_level][nvarchar](max) NULL,
//    [linkedin_url][nvarchar](max) NULL,
//    [skills][nvarchar](max) NULL,
//    [past_companies][nvarchar](max) NULL,
//    [company_name][nvarchar](max) NULL,
//    [company_size][nvarchar](max) NULL,
//    [company_website][nvarchar](max) NULL,
//    [company_phone_numbers][nvarchar](max) NULL,
//    [company_location_text][nvarchar](max) NULL,
//    [company_type][nvarchar](max) NULL,
//    [company_function][nvarchar](max) NULL,
//    [company_sector][nvarchar](max) NULL,
//    [company_industry][nvarchar](max) NULL,
//    [company_founded_at][nvarchar](max) NULL,
//    [company_funding_range][nvarchar](max) NULL,
//    [revenue_range][nvarchar](max) NULL,
//    [ebitda_range][nvarchar](max) NULL,
//    [company_facebook_page][nvarchar](max) NULL,
//    [company_twitter_page][nvarchar](max) NULL,
//    [company_linkedin_page][nvarchar](max) NULL,
//    [company_sic_code][nvarchar](max) NULL,
//    [company_naics_code][nvarchar](max) NULL,
//    [company_description][nvarchar](max) NULL,
//    [company_size_key][nvarchar](max) NULL,
//    [company_profile_image_url][nvarchar](max) NULL,
//    [company_products_services][nvarchar](max) NULL,
//    [keywords][nvarchar](max) NULL,
// CONSTRAINT[PK_tblContacts] PRIMARY KEY CLUSTERED
//(
//    [Ids] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
//) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]
//GO


