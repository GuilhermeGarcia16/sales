using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
            //FORMVIEWMODEL
            //É UM MODEL COMPOSTO MUITO ÚTIL PARA TELAS QUE PRECISAM DE DADOS DE DIFERENTES ENTIDADES, NO CASO AQUI HÁ UM CAMPO SELECT PARA DEPARTAMENTS
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormVIewModel() { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//CRSF
        public IActionResult Create(Seller seller) 
        {
            //Gravando no banco
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) 
        { 
            if (id == null){ return NotFound(); }

            var obj = _sellerService.FindBy(id.Value);

            if (obj == null) { return NotFound(); }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var obj = _sellerService.FindBy(id.Value);

            if (obj == null) { return NotFound(); }

            return View(obj);
        }
    }
}
