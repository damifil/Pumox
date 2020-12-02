using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecruitTask.Models;
using RecruitTask.Models.Entities;
using RecruitTask.Models.Entities.dbo;
using RecruitTask.Models.Services;

namespace RecruitTask.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICompanyService companyService;
        public CompanyController(IMapper mapper, ICompanyService companyService)
        {
            this.mapper = mapper;
            this.companyService = companyService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CompanyRequest companyRequest)
        {
            var company = new Company();
            mapper.Map(companyRequest, company);

            var result = companyService.CreateCompany(company);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Search([FromBody] CompanyFilterRequest model)
        {
            if (!model.RangeOfBirthDateIsCorrect())
                return new BadRequestObjectResult("Range of date is incorrect");

            var filter = new CompanyFilter();
            mapper.Map(model, filter);

            var result = companyService.SearchCompany(filter);
            var companyRequest = new List<CompanyRequest>();

            mapper.Map(result, companyRequest);
            return Ok(JsonConvert.SerializeObject(companyRequest));
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromQuery] int id, [FromBody] CompanyRequest companyRequest)
        {
            var company = new Company();
            mapper.Map(companyRequest, company);

            var result = companyService.UpdateCompany(company, id);
            if (result)
                return Ok();
            else
                return new BadRequestObjectResult("Provided id doesn't match any company");

        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            var result = companyService.DeleteCompany(id);
            if (result)
                return Ok();
            else
                return new BadRequestObjectResult("Provided id doesn't match any company");

        }
    }
}
