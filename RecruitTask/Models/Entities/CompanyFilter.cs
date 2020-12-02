using RecruitTask.Models.Entities.dbo;
using System;
using System.Collections.Generic;

namespace RecruitTask.Models.Entities
{
    public class CompanyFilter
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public List<EmployerJobTitle> EmployeeJobTitles { get; set; }
    }
}
