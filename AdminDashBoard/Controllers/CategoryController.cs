using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Repository;

namespace AdminDashBoard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOf;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOf, IMapper mapper)
        {
            _unitOf = unitOf;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var category = await _unitOf.Repository<Category, int>().GetAllAsync();
            return View(category);
        }
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                await _unitOf.Repository<Category, int>().AddAsync(category);
                await _unitOf.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(" " ,ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var cate = await _unitOf.Repository<Category, int>().GetByIdAsync(id);
            _unitOf.Repository<Category, int>().Delete(cate);
            await _unitOf.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
