using AutenticaNET.API.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutenticaNET.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IAuthService _authService;

    public UsersController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetPerfil()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(idClaim, out _))
            return Unauthorized();

        var usuario = await _authService.GetByIdAsync(idClaim!);
        if (usuario == null)
            return NotFound();

        return Ok(new
        {
            usuario.Id,
            NomeUsuario = usuario.Username,
            usuario.Email
        });
    }
}
