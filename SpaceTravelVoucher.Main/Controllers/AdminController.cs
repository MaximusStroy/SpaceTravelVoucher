using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.Main.Data;
using SpaceTravelVoucher.Main.Models;

namespace SpaceTravelVoucher.Main.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppUserDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public AdminController(AppUserDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
        }

        #region Table "Users"
        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return _context.VUsers != null ?
                        View(await _context.VUsers.ToListAsync()) :
                        Problem("Entity set 'AppUserDbContext.VUsers'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VUsers == null)
            {
                return NotFound();
            }

            var vUsers = await _context.VUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vUsers == null)
            {
                return NotFound();
            }

            return View(vUsers);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,Email,PhoneNumber,LastName,UserName,Id,Name")] VUsers vUsers)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Email = vUsers.Email;
                user.FirstName = vUsers.FirstName;
                user.LastName = vUsers.LastName;
                user.PhoneNumber = vUsers.PhoneNumber;
                user.UserName = vUsers.UserName;
                //var result = await _userManager.CreateAsync(user, Input);

                _context.Add(vUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vUsers);
        }


        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VUsers == null)
            {
                return NotFound();
            }

            var vUsers = await _context.VUsers.FindAsync(id);
            if (vUsers == null)
            {
                return NotFound();
            }
            return View(vUsers);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,Email,PhoneNumber,LastName,UserName,Id,Name")] VUsers vUsers)
        {
            if (id != vUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var role = "";
                    switch (vUsers.Name)
                    {
                        case "0": role = "Admin"; break;
                        case "1": role = "Manager"; break;
                        case "2": role = "Member"; break;
                    }
                    var oldUser = await _userManager.FindByIdAsync(vUsers.Id);
                    var roles = _userManager.GetRolesAsync(oldUser); 
                    if (roles != null)
                    {
                        foreach (var item in await roles)
                            await _userManager.RemoveFromRoleAsync(oldUser, item);

                        await _userManager.AddToRoleAsync(oldUser, role);
                    }
                    oldUser.PhoneNumber = vUsers.PhoneNumber;
                    oldUser.LastName = vUsers.LastName;
                    oldUser.FirstName = vUsers.FirstName;
                    oldUser.Email = vUsers.Email;

                    await _userManager.UpdateSecurityStampAsync(oldUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VUsersExists(vUsers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vUsers);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VUsers == null)
            {
                return NotFound();
            }

            var vUsers = await _context.VUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vUsers == null)
            {
                return NotFound();
            }

            return View(vUsers);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VUsers == null)
            {
                return Problem("Entity set 'AppUserDbContext.VUsers'  is null.");
            }
            var vUsers = await _context.VUsers.FindAsync(id);
            if (vUsers != null)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == vUsers.Id);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    var userId = await _userManager.GetUserIdAsync(user);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred deleting user.");
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VUsersExists(string id)
        {
            return (_context.VUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion

        #region Table "Protocol"
        #endregion
    }

}