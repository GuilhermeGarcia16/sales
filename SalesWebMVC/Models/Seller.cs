using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public String Name { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0,50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public Double BaseSalary { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [DataType (DataType.Date)]
        public DateTime BirthDay { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        public int DepartmentId { get; set; }
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
