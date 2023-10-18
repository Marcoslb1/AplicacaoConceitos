using Conceitos.Data;
using Conceitos.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Conceitos.Controllers
{

    public class TarefaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger _log;

        public TarefaController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ILogger<UsuarioController> logger, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _log = logger;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(Tarefa tarefa)
        {
            return LocalRedirect("/");
        }

        public async Task<IActionResult> DeletarAsync(int id)
        {
            var tarefa = await _db.Tarefas.Where(t => t.Id == id).FirstOrDefaultAsync();

            if(tarefa == null)
            {
                ModelState.AddModelError("Erro", "Não foi possível identificar a tarefa.");
                return UnprocessableEntity(ModelState);
            }

            _db.Tarefas.Remove(tarefa);
            _db.SaveChanges();

            return LocalRedirect("/tarefas");
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarAsync(int id)
        {
            return LocalRedirect("/");
        }
    }
}
