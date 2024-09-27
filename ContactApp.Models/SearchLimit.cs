using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    public class SearchLimit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SearchLimitCount { get; set; }
        public DateTime SearchLimitDate { get; set; }
        public int SearchBy { get; set; }

        public string Email { get; set; }
        public string password { get; set; }


    }
}
