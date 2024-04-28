using Dapper.Contracts;
using Dapper1.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapper1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _compnyRepo;

        public CompaniesController(ICompanyRepository compnyRepo)
        {
            _compnyRepo = compnyRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var comapnies = await _compnyRepo.GetCompanies();
            return Ok(comapnies);
        }

        [HttpGet("{id}",Name ="CompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _compnyRepo.GetCompany(id);
            if(company==null)
            {
                return NotFound();
            }
            return Ok(company);
        }


        [HttpPost]
        public async Task<IActionResult> CreatedCompany([FromBody]CompanyForCreationDto company)
        {
            var createdcompany = await _compnyRepo.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdcompany.Id }, createdcompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyForUpdateDto company)
        {
            var dbcompany = await _compnyRepo.GetCompany(id);
            if (dbcompany is null)
                return NotFound();
            await _compnyRepo.UpdateCompany(id, company);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var dbcompany = await _compnyRepo.GetCompany(id);
            if (dbcompany is null)
                return NotFound();
            await _compnyRepo.DeleteCompany(id);
            return NoContent();
        }
        [HttpGet("ByEmployeeId/{id}")]
        public async Task<IActionResult> GetCompanyForEmployee(int id)
        {
            var company = await _compnyRepo.GetCompanyByEmployeeId(id);
            if (company is null)
                return NotFound();
            return Ok(company);
        }
    }
}
