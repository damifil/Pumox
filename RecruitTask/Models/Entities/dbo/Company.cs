using FluentNHibernate.Mapping;
using System.Collections.Generic;


namespace RecruitTask.Models.Entities.dbo
{
    public class Company
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int EstablishmentYear { get; set; }
        public virtual IList<Employer> Employees { get; set; }
    }

    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(255);
            Map(x => x.EstablishmentYear).Not.Nullable();
            HasMany(x => x.Employees).KeyColumn("CompanyId").Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
