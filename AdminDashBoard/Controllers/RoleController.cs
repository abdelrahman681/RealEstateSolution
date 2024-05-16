using AdminDashBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace AdminDashBoard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            //Get All Roles
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel viewModel)
        {
            //check the model state is valid
            if(ModelState.IsValid)
            {
                //check the role is Exists or not
                var roleExists = await _roleManager.RoleExistsAsync(viewModel.Name);
                //if not Exists will create
                if(!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole  (viewModel.Name.Trim() ));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "The Role is Exists");
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            //Get  Rol by id and delete
            var roles = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(roles);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            //Get  Rol by id and delete
            var roles = await _roleManager.FindByIdAsync(id);
            var MappedRole = new EditRoleViewModel
            {
                Name=roles.Name,
            };
           return View(MappedRole);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,EditRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                //check the role is Exists or not
                var roleExists = await _roleManager.RoleExistsAsync(model.Name);
                if(!roleExists)
                {
                    var role =await _roleManager.FindByIdAsync (model.Id);
                    //change the name was send by new name is sending
                    role.Name = model.Name;
                    //update roel
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "The Role is  Exists");
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
