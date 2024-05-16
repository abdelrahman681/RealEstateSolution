using AdminDashBoard.Helper;
using AdminDashBoard.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.DTO;
using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Repository;
using RealEstate.Reopsitory.Specification;
using System.Threading.Tasks;
namespace AdminDashBoard.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IUnitOfWork _unitOf;
        private readonly IMapper _mapper;

        public PropertyController(IUnitOfWork unitOf, IMapper mapper)
        {
            _unitOf = unitOf;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            //get all Homes 
            var propert = await _unitOf.Repository<Property, int>().GetAllAsync();
            var mappedprop = _mapper.Map<IReadOnlyList<Property>, IReadOnlyList<PropertyViewModel>>(propert);
            return View(mappedprop);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PropertyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Image != null)
                {
                    viewModel.PictureUrl = PictureSetting.UploadPicture(viewModel.Image);
                }
                else
                {
                    viewModel.PictureUrl = @"Images/9ebcc941.jpg";
                }
                var maped = _mapper.Map<PropertyViewModel, Property>(viewModel);
                await _unitOf.Repository<Property, int>().AddAsync(maped);
                await _unitOf.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var spec = new PropertySpecification(id);
            var prop = await _unitOf.Repository<Property, int>().GetByIdWithSpecificationAsync(spec);

            var maper = _mapper.Map<Property, PropertyViewModel>(prop);
            return View(maper);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PropertyViewModel model, int id)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    if (model.PictureUrl != null)
                    {
                        PictureSetting.DeleteFile(model.PictureUrl);
                        model.PictureUrl = PictureSetting.UploadPicture(model.Image);
                    }
                    else
                        model.PictureUrl = PictureSetting.UploadPicture(model.Image);
                    var mapped = _mapper.Map<PropertyViewModel, Property>(model);

                    _unitOf.Repository<Property,int>().Update(mapped);
                    if(await _unitOf.CompleteAsync() > 0)
                        return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete( int id)
        {
            var spec = new PropertySpecification(id);
            var prop = await _unitOf.Repository<Property, int>().GetByIdWithSpecificationAsync(spec);
            var mapp = _mapper.Map<PropertyViewModel>(prop);
            return View(mapp);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id,PropertyViewModel viewModel)
        {
            if(id!=viewModel.Id) return NotFound();

            try
            {
                var spec = new PropertySpecification(id);
                var prop = await _unitOf.Repository<Property, int>().GetByIdWithSpecificationAsync(spec);
                if (viewModel.PictureUrl != null)
                    PictureSetting.DeleteFile(viewModel.PictureUrl);

                _unitOf.Repository<Property,int>().Delete(prop);
                await _unitOf.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(" ", ex.Message);
            }
            return View(viewModel);
        }

    }
}
