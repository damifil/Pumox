using System;
using System.Collections.Generic;
using RecruitTask.Models.Entities.dbo;


namespace RecruitTask.Models
{
    public class CompanyRequest
    {
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public List<EmployerRequest> Employees { get; set; }
    }

    public class EmployerRequest
    {
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  DateTime DateOfBirth { get; set; }
        public  EmployerJobTitle JobTitle { get; set; }
    }
}
