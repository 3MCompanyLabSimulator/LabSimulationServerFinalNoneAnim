using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSimulation.Models
{
    public class gmail
    {

        public string To { get; set; }
        [Required]
        [EmailAddress]
        public string From { get; set; }

        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }

    }
}