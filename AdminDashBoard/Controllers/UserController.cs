using AdminDashBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entiry.IdentityEntity;

namespace AdminDashBoard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _role;

        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> role)
        {
            _userManager = userManager;
            _role = role;
        }
        public async Task<IActionResult> Index()
        {
            //Get All User
            var user = await _userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                DisplayName = u.DisplayName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserName = u.UserName,
                Roles=_userManager.GetRolesAsync(u).Result
            }).ToListAsync();
            return View(user);
        }
        public async Task<IActionResult> Edit(string id)
        {
            //Get  User
            var user = await _userManager.FindByIdAsync(id);
            var roles=await _role.Roles.ToListAsync();

            var ViewModel = new UsersRolesViewModel
            {
                UserId = user.Id,
                UserName = user.DisplayName,
                Roles = roles.Select(r => new EditRoleViewModel()
                {
                    Id=r.Id,
                    Name=r.Name,
                    //if isSelected true means the user have roles
                    IsSelected=_userManager.IsInRoleAsync(user,r.Name).Result
                }).ToList()
            };
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UsersRolesViewModel viewModel)
        {
            var user=await _userManager.FindByIdAsync(viewModel.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in viewModel.Roles)
            {
                if (roles.Any(r => r == role.Name) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (!roles.Any(r => r == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.Name);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
