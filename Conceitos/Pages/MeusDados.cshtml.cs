using Conceitos.Data;
using Conceitos.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Conceitos.Pages
{
    public class MeusDadosModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _db;

        public MeusDadosModel(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        //Ao realizar alterações no front que contenha o valor nesse objeto, o valor automáticamente também é alterado aqui
        [BindProperty]
        public Usuario usuario { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Usuario content = await _userManager.GetUserAsync(User);
            content.CEP = usuario.CEP;
            content.Endereco = usuario.Endereco;
            content.Cidade = usuario.Cidade;
            content.Estado = usuario.Estado;
            content.Bairro = usuario.Bairro;
            content.Numero = usuario.Numero;

            if (!String.IsNullOrEmpty(usuario.Senha))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(content);
                var r = await _userManager.ResetPasswordAsync(content, token, usuario.Senha);
            }
            var result = await _userManager.UpdateAsync(content);

            return RedirectToPage("/");
        }
    }
}
