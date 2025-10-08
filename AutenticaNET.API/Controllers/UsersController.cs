using AutenticaNET.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutenticaNET.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetPerfil()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(idClaim)) return Unauthorized();

            var usuario = await _db.Users.FindAsync(idClaim);
            if (usuario == null) return NotFound();

            return Ok(new
            {
                usuario.Id,
                NomeUsuario = usuario.Username,
                usuario.Email
            });
        }
    }
}
