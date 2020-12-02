using NHibernate;
using RecruitTask.Models.Entities;
using RecruitTask.Models.Entities.dbo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitTask.Models.Repositories
{
    public interface ICompanyRepository
    {
        public Company GetCompany(int id);
        public List<Company> GetCompany(CompanyFilter filterValue);

        public bool DeleteCompany(int id);
        public int CreateCompany(Company company);
        public bool UpdateCompany(Company company, int id);

    }
    public class CompanyRepository : ICompanyRepository
    {
        private ISession session;

        public CompanyRepository(ISession session)
        {
            this.session = session;

        }
        public int CreateCompany(Company company)
        {
            using (session)
            {
                foreach (var item in company.Employees)
                {
                    item.Company = company;
                }

                session.Save(company);

                return company.Id;
            }
        }

        public bool DeleteCompany(int id)
        {
            if (GetCompany(id) == null)
                return false;

            var company = GetCompany(id);

            session.Delete(company);
            session.Flush();

            return true;
        }

        public Company GetCompany(int id)
        {
            return session.Get<Company>(id);
        }

        public List<Company> GetCompany(CompanyFilter filterValue)
        {
            var querry = session.Query<Company>();
            if (!String.IsNullOrEmpty(filterValue.Keyword))
            {
                var keyword = filterValue.Keyword;
                querry = querry.Where(c => c.Name.Contains(keyword)
                                           || c.Employees.Any(e => e.FirstName.Contains(keyword) || e.LastName.Contains(keyword)));
            }

            var dateTimeFrom = filterValue.EmployeeDateOfBirthFrom ?? DateTime.MinValue;
            var dateTimeTo = filterValue.EmployeeDateOfBirthTo ?? DateTime.Now;

            querry = querry.Where(c => c.Employees.Any(e => e.DateOfBirth >= dateTimeFrom && e.DateOfBirth <= dateTimeTo));

            if (filterValue.EmployeeJobTitles != null && filterValue.EmployeeJobTitles.Any())
            {
                querry = querry.Where(c => c.Employees.Any(e => filterValue.EmployeeJobTitles.Contains(e.JobTitle)));
            }

            return querry.ToList();
        }

        public bool UpdateCompany(Company company, int id)
        {
            var fromDb = session.Query<Company>().Where(c => c.Id == id).FirstOrDefault();
            if (fromDb == null)
                return false;

            fromDb.Name = company.Name;
            fromDb.EstablishmentYear = company.EstablishmentYear;

            var itemsToRemove = fromDb.Employees.ToArray();
            foreach (var item in itemsToRemove)
            {
                fromDb.Employees.Remove(item);
            }

            foreach (var item in company.Employees)
            {
                item.Company = fromDb;
                fromDb.Employees.Add(item);
            }

            session.Update(fromDb);
            session.Flush();
            return true;
        }
    }
}
