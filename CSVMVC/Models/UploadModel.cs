using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSVMVC.Models
{
    public class UploadModel
    {
        public int ID { get; set; }

        [Required]
        public string Payroll_Number { get; set; }

        [Required]
        public string Forenames { get; set; }

        [Required]
        public string Surname { get; set; }
        
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of Birth is required")]

        public DateTime? Date_of_Birth { get; set; }

        [Required]
        public int Telephone { get; set; }

        [Required]
        public int Mobile { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Address_2 { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string EMail_Home { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime? Start_Date { get; set; }
    }
}