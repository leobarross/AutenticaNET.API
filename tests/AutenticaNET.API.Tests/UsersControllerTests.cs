using AutenticaNET.API.Controllers;
using AutenticaNET.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace AutenticaNET.API.Tests;

public class UsersControllerTests
{
    private static UsersController CreateController(string? id, User? user = null)
    {
        var fake = new AuthControllerTests.FakeAuthService { User = user };
        var claims = id == null ? Array.Empty<Claim>() :
            new[] { new Claim(ClaimTypes.NameIdentifier, id) };
        return new UsersController(fake)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"))
                }
            }
        };
    }

    [Fact]
    public async Task Perfil_SemIdentificador_Retorna401()
    {
        Assert.IsType<UnauthorizedResult>(await CreateController(null).GetPerfil());
    }

    [Fact]
    public async Task Perfil_ComIdentificadorInvalido_Retorna401()
    {
        Assert.IsType<UnauthorizedResult>(await CreateController("nao-e-guid").GetPerfil());
    }

    [Fact]
    public async Task Perfil_UsuarioNaoEncontrado_Retorna404()
    {
        Assert.IsType<NotFoundResult>(await CreateController(Guid.NewGuid().ToString()).GetPerfil());
    }

    [Fact]
    public async Task Perfil_UsuarioEncontrado_Retorna200()
    {
        var user = new User("teste", "teste@example.com", "hash");
        Assert.IsType<OkObjectResult>(await CreateController(user.Id, user).GetPerfil());
    }
}
