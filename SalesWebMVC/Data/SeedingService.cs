using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;
        //Injetar a dependência do context
        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if( _context.Department.Any() || 
                _context.Seller.Any() ||
                _context.SalesRecords.Any())
            {
                return; //DB já foi populado
            }

            //Seeding

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", 1000.0, new DateTime(1998, 4, 21), d1);
            Seller s2 = new Seller(2, "Ana Clara", "ana@gmail.com", 2000.0, new DateTime(1994, 3, 1), d2);
            Seller s3 = new Seller(3, "Tyler Grey", "tyler@gmail.com", 3000.0, new DateTime(1995, 7, 6), d3);
            Seller s4 = new Seller(4, "Alex Pink", "alex@gmail.com", 4000.0, new DateTime(1990, 10, 12), d4);

            SalesRecord sl1 = new SalesRecord(1, new DateTime(2018, 9, 25), 11000.0, SaleStatus.Billed, s1);
            SalesRecord sl2 = new SalesRecord(2, new DateTime(2018, 10, 20), 11000.0, SaleStatus.Billed, s2);
            SalesRecord sl3 = new SalesRecord(3, new DateTime(2018, 1, 5), 11000.0, SaleStatus.Billed, s3);

            //Adicionar os objetos no banco de dados
            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecords.AddRange(sl1, sl2, sl3);

            _context.SaveChanges();

        }
    }
}
