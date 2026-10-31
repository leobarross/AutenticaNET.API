using AutenticaNET.API.Controllers;
using AutenticaNET.API.DTOs;
using AutenticaNET.API.Models;
using AutenticaNET.API.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AutenticaNET.API.Tests;

public class AuthControllerTests
{
    [Fact]
    public async Task Login_QuandoCredenciaisInvalidas_Retorna401()
    {
        var controller = new AuthController(new FakeAuthService { Token = null });
        var result = await controller.Login(new LoginDto { Email = "teste@example.com", Password = "senha" });
        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public async Task Login_QuandoValido_RetornaToken()
    {
        var controller = new AuthController(new FakeAuthService { Token = "token-de-teste" });
        var result = await controller.Login(new LoginDto { Email = "teste@example.com", Password = "senha" });
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Registro_Duplicado_Retorna409()
    {
        var controller = new AuthController(new FakeAuthService { Duplicado = true });
        var result = await controller.Register(new RegisterDto
        {
            Username = "teste", Email = "teste@example.com", Password = "Senha1234"
        });
        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async Task Registro_Valido_Retorna201()
    {
        var controller = new AuthController(new FakeAuthService());
        var result = await controller.Register(new RegisterDto
        {
            Username = "teste", Email = "teste@example.com", Password = "Senha1234"
        });
        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(201, response.StatusCode);
    }

    internal sealed class FakeAuthService : IAuthService
    {
        public string? Token { get; set; } = "token-de-teste";
        public bool Duplicado { get; set; }
        public User? User { get; set; }

        public Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            if (Duplicado) throw new InvalidOperationException("Email já cadastrado");
            return Task.FromResult(new UserDto
            {
                Id = Guid.NewGuid().ToString(), Username = dto.Username, Email = dto.Email
            });
        }

        public Task<string> LoginAsync(LoginDto dto) => Task.FromResult(Token!);
        public Task<User> GetByIdAsync(string id) => Task.FromResult(User!);
    }
}
