using Dapper.Context;
using Dapper.Contracts;
using Dapper.Model;
using Dapper1.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace Dapper.Repository
{
    public class CompanyRepository :ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateCompany(CompanyForCreationDto Company)
        {
            var query = "Insert into companies(Name,Address,Country) values(@Name,@Address,@Country)"   +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", Company.Name, DbType.String);
            parameters.Add("@Address", Company.Address, DbType.String);
            parameters.Add("@Country", Company.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                // Assuming you have a database connection named 'connection'
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var CreatedCompany = new Company
                {
                    Id = id,
                    Name = Company.Name,
                    Address = Company.Address,
                    Country = Company.Country

                };
                return CreatedCompany;
            }
        }

        public async Task DeleteCompany(int id)
        {
            var query = "Delete from Companies where Id=@Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
            }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
           var query= "select * from Companies";
            using(var connection=_context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "select * from companies where Id=@Id";
            using(var connection=_context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
                return company;
            }
        }

        public async Task<Company> GetCompanyByEmployeeId(int id)
        {
            var procedureName = "ShowCompanyByEmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Company>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company;
            }
            }

        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = "UPDATE Companies SET Name=@Name,Address=@Address,Country=@Country where Id=@Id ";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }


            }
    }
}
