using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAssignment.Models
{
    public class product
    {
        public Int64 ProductID { get; set; }

        [Required(ErrorMessage ="Please Enter the Product Name")]
        public string productName { get; set; }

        [Required(ErrorMessage ="Please Enter The Price")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage ="Please Enter Quantity")]
        [RegularExpression(@"^([0-9])$",ErrorMessage ="Please Enter Number only")]
        public Int64? Quantity { get; set; }


        public bool? IsGSTApplicable { get; set; }

        [Required(ErrorMessage ="Please Select Purchase Date.")]
        public string Purchase_Date { get; set; }

        [Required(ErrorMessage = "Please Select Expiry Date.")]

        public string Expiry_Date { get; set; }

        public Int64? ColorID { get; set; }
        public string ColorName { get; set; }

    }
}