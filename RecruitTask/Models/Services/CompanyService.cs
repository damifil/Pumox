using System.Collections.Generic;
using RecruitTask.Models.Entities;
using RecruitTask.Models.Entities.dbo;
using RecruitTask.Models.Repositories;


namespace RecruitTask.Models.Services
{
    public interface ICompanyService
    {
        public int CreateCompany(Company company);
        public List<Company> SearchCompany(CompanyFilter filter);
        public bool UpdateCompany(Company company, int id);

        public bool DeleteCompany(int id);
    }
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository repository;
        public CompanyService(ICompanyRepository repository)
        {
            this.repository = repository;
        }
        public int CreateCompany(Company company)
        {
            return repository.CreateCompany(company);
        }

        /// <returns>return true if company with id exist</returns>
        public bool DeleteCompany(int id)
        {
            return repository.DeleteCompany(id);
        }

        public List<Company> SearchCompany(CompanyFilter filter)
        {
            return repository.GetCompany(filter);
        }

        /// <returns>return true if company with id exist</returns>
        public bool UpdateCompany(Company company, int id)
        {
            return repository.UpdateCompany(company, id);
        }
    }
}
