using Conceitos.Data;
using Conceitos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conceitos.Pages
{
    [Authorize]
    public class TarefasModel : PageModel
    {
        public List<Tarefa> tarefas;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _db;

        public TarefasModel(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return RedirectToPage();
            }

            tarefas = await _db.Tarefas.Where(user => user.Usuario.Id == usuario.Id.ToString()).ToListAsync();

            return Page();
        }
    }
}
