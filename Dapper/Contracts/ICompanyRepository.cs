using Dapper.Model;
using Dapper1.DTO;

namespace Dapper.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForCreationDto Company);

        public Task UpdateCompany(int id, CompanyForUpdateDto company);

        public Task DeleteCompany(int id);

        public Task<Company> GetCompanyByEmployeeId(int id);


    }
}
