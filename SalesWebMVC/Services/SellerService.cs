using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindBy(int id) 
        {
            //Eager Loading => No link "Details", o Department não está associado na exibição, pra isso importamos o Microsoft.EntityFrameworkCore e usamos o Include (abaixo) pra fazer o join no banco de dados
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
        }
        public void Remove(int id) 
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }   
}
