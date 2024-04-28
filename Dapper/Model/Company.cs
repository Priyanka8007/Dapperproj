namespace Dapper.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { set; get; }

        public string? Country { set; get; }
        public List<Employee> Employees { set; get; } = new List<Employee>();
    }
}
