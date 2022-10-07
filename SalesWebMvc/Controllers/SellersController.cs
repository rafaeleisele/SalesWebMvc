using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        //private readonly SalesWebMvcContext _context;

        public SellersController(DepartmentService departmentService, SellerService sellerService /*, SalesWebMvcContext contexto*/)
        {
            //_sellerService = new SellerService(contexto);
            _sellerService = sellerService;
            _departmentService = departmentService;
            
            //_context = contexto;
        }

        public IActionResult Index()
        {
            //var sellerService = new SellerService(_context);
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            //var sellerService = new SellerService(_context);
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            //var sellerService = new SellerService(_context);
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(Id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            _sellerService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
