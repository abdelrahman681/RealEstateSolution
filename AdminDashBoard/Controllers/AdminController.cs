using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.DTO;
using RealEstate.Domain.Entiry.IdentityEntity;

namespace AdminDashBoard.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user=await _userManager.FindByEmailAsync(login.Email);
            if(user is null)
            {
                ModelState.AddModelError("Email", "InValid Email");
                return RedirectToAction(nameof(Login));
            }
            var password = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!password.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError("", "You Ara not Admin");
                return RedirectToAction(nameof(Login));
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
