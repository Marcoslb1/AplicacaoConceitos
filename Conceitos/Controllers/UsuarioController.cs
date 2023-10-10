using Microsoft.AspNetCore.Mvc;
using Conceitos.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Conceitos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger _log;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ILogger<UsuarioController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _log = logger; 
        }
        public class Login
        {
            [Required(ErrorMessage = "O campo e-mail é obrigatório")]
            [EmailAddress(ErrorMessage = "Preencha o campo e-mail corretamente")]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Senha { get; set; }
            public bool RememberMe { get; set; }
        }
        [HttpPost]
        [Route("Usuario/buscarCep")]
        //[frombody] -> receber informações do ajax nos formatos json/xml
        public IActionResult buscarCep([FromBody] string cep)
        {
            return Ok();
        }
        [HttpPost]
        [Route("Usuario/cadastrar")]
        //[fromform] -> receber informações por meio de formulários formData
        public async Task<IActionResult> cadastrarAsync([FromForm] Usuario usuario)
        {
            usuario.UserName = usuario.Email;
            var result = await _userManager.CreateAsync(usuario, usuario.Senha);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Ok(usuario);
        }

        public async Task<IActionResult> logoutAsync()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }

        public async Task<IActionResult> loginAsync(Login login)
        {
            if (ModelState.IsValid)
            {
                //verifica se está bloqueado
                //var isLockedOut = await _userManager.IsLockedOutAsync(usuario);
                
                //desbloquear usuário
                //await _userManager.SetLockoutEndDateAsync(usuario, DateTimeOffset.UtcNow);
                
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, login.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return LocalRedirect("/");
                }
                if (result.IsLockedOut || result.IsNotAllowed)
                {
                    ModelState.AddModelError("UserLocked", "Seu cadastro precisa ser ativado.");

                    return UnprocessableEntity(ModelState);
                }
                else
                {
                    ModelState.AddModelError("InvalidLogin", "Usuário ou senha não encontrados. Por favor, verifique os dados e tente novamente.");

                    return UnprocessableEntity(ModelState);
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }
    }
}
