namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String  Email { get; set; }
        public Double BaseSalary { get; set; }
        public DateTime BirthDay { get; set; }

        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDay, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDay = birthDay;
            Department = department;
        }

        public void AddSales(SalesRecord sales)
        {
            Sales.Add(sales);
        }

        public void RemoveSales(SalesRecord sales)
        {
            Sales.Remove(sales);
        }

        public Double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr=>sr.Amount);
        }
    }
}
