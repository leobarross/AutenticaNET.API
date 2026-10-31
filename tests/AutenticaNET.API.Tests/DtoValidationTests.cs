using AutenticaNET.API.DTOs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AutenticaNET.API.Tests;

public class DtoValidationTests
{
    private static bool IsValid(object dto)
    {
        var context = new ValidationContext(dto);
        return Validator.TryValidateObject(dto, context, new List<ValidationResult>(), true);
    }

    [Fact]
    public void Registro_RejeitaEmailInvalido()
    {
        Assert.False(IsValid(new RegisterDto
        {
            Username = "teste", Email = "nao-e-email", Password = "Senha1234"
        }));
    }

    [Fact]
    public void Registro_RejeitaSenhaCurta()
    {
        Assert.False(IsValid(new RegisterDto
        {
            Username = "teste", Email = "teste@example.com", Password = "123"
        }));
    }

    [Fact]
    public void Login_ExigeSenha()
    {
        Assert.False(IsValid(new LoginDto
        {
            Email = "teste@example.com", Password = ""
        }));
    }

    [Fact]
    public void Registro_ComDadosValidos_PassaNaValidacao()
    {
        Assert.True(IsValid(new RegisterDto
        {
            Username = "teste", Email = "teste@example.com", Password = "Senha1234"
        }));
    }
}
