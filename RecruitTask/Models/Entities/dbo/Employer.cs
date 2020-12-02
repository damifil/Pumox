using FluentNHibernate.Mapping;
using System;

namespace RecruitTask.Models.Entities.dbo
{
    public class Employer
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual EmployerJobTitle JobTitle { get; set; }
        public virtual Company Company { get; set; }
    }

    public class EmployerMap : ClassMap<Employer>
    {
        public EmployerMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName).Not.Nullable().Length(81);
            Map(x => x.LastName).Not.Nullable().Length(255);
            Map(x => x.DateOfBirth).Not.Nullable();
            Map(x => x.JobTitle);
            References(x => x.Company, "CompanyId").Cascade.None();
        }
    }

    public enum EmployerJobTitle
    {
        Administrator, Developer, Architect, Manager
    }
}
