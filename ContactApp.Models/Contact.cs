using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    public class Contact
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ids { get; set; }

        public string? id { get; set; } 
        public string?  company_id { get; set; } 

        [Display(Name ="name")] 
        public string? name { get; set; }
        
        [Display(Name = "email")]
       
        public string? email { get; set; }
        [Display(Name = "email_score")]
       
        public string? email_score { get; set; }
        [Display(Name = "phone")]
        public string? phone { get; set; }
        [Display(Name = "Work Phone")]
        public string? work_phone { get; set; }

        [Display(Name = "lead_location")]
       
        public string? lead_location { get; set; }
        [Display(Name = "lead_division")]
        public string? lead_division { get; set; }
        [Display(Name = "lead_titles")]
        public string? lead_titles { get; set; }
        [Display(Name = "decision_making_power")]
        public string? decision_making_power { get; set; }
        [Display(Name = "seniority_level")]
        public string? seniority_level { get; set; }
       
        [Display(Name = "linkedin_url")]
        public string? linkedin_url { get; set; }

        [Display(Name = "skills")]
        public string? skills { get; set; }
        [Display(Name = "past_companies")]
        public string? past_companies { get; set; }
        [Display(Name = "company_name")]
        public string? company_name { get; set; }
        [Display(Name = "company_size")]
        public string? company_size { get; set; }
        [Display(Name = "company_website")]
        public string? company_website { get; set; }
        [Display(Name = "company_phone_numbers")]
        public string? company_phone_numbers { get; set; }
        [Display(Name = "company_location_text")]
        public string? company_location_text { get; set; }
        [Display(Name = "company_type")]
        public string? company_type { get; set; }
        [Display(Name = "company_function")]
        public string? company_function { get; set; }
        [Display(Name = "company_sector")]
        public string? company_sector { get; set; }

        [Display(Name = "company_industry")]
        public string? company_industry { get; set; }
        [Display(Name = "company_founded_at")]
        public string? company_founded_at { get; set; }
        [Display(Name = "company_funding_range")]
        public string? company_funding_range { get; set; }
        [Display(Name = "revenue_range")]
        public string? revenue_range { get; set; }
        [Display(Name = "ebitda_range")]
        public string? ebitda_range { get; set; }

        [Display(Name = "company_facebook_page")]
        public string? company_facebook_page { get; set; }
        [Display(Name = "company_twitter_page")]
        public string? company_twitter_page { get; set; }
        [Display(Name = "company_linkedin_page")]
        public string? company_linkedin_page { get; set; }
        [Display(Name = "company_sic_code")]
        public string? company_sic_code { get; set; }
        [Display(Name = "company_naics_code")]
        public string? company_naics_code { get; set; }

        [Display(Name = "company_description")]
        public string? company_description { get; set; }
        [Display(Name = "company_size_key")]
        public string? company_size_key { get; set; }
        [Display(Name = "company_profile_image_url")]
        public string? company_profile_image_url { get; set; }

       
       }

}
