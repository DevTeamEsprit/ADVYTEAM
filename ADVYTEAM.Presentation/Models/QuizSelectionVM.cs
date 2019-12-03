using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Models
{
    public class QuizSelectionVM
    {
        // Selection Properties
        public string SelectedCategory { get; set; }
        public string SelectedSkill { get; set; }
        
        // Enumerables
        public IEnumerable<SelectListItem> CategoryItems { get; set; }
        public IEnumerable<SelectListItem> SkillItems { get; set; }

        public IEnumerable<SelectListItem> dropDownCategories { get; set; }
        public IEnumerable<SelectListItem> dropDownSkills { get; set; }

        public List<skill> allSkills { get; set; }
    }
}