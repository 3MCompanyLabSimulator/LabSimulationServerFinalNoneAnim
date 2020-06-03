using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabSimulation.Models
{
    public class HomeIndexVM
    {
        public List<View_ExperienceInfo> ExperienceInfo { get; set; }
        public List<View_EducationalLeve_CountExperience> EducationalLeve_CountExperience { get; set; }

        public View_ExperienceInfo ExperienceDetails { get; set;}
        public List<View_Comments_Users> comments { get; set; }

    }
}