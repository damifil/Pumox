using RecruitTask.Models.Entities.dbo;
using System;
using System.Collections.Generic;

namespace RecruitTask.Models
{
    public class CompanyFilterRequest
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public List<EmployerJobTitle> EmployeeJobTitles { get; set; }

        public bool RangeOfBirthDateIsCorrect()
        {
            if(EmployeeDateOfBirthFrom.HasValue && EmployeeDateOfBirthTo.HasValue)
            {
                return EmployeeDateOfBirthFrom.Value <= EmployeeDateOfBirthTo.Value;
            }

            return true;
        }
    }
}
