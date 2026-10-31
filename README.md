# AutenticaNET.API

Sistema de autenticação com **JWT** usando **ASP.NET Core** e **SQL Server**.

Este projeto é um exemplo prático de backend com APIs REST, implementando funcionalidades essenciais de autenticação e segurança.

---

## Funcionalidades

- Registro de usuários com **hash de senha** usando **BCrypt**  
- Login e geração de **JWT**  
- Endpoint protegido para visualizar perfil do usuário (`profile`)  
- Controle de roles (`User`)  
- Banco de dados **SQL Server**  
- Estrutura organizada para facilitar manutenção e evolução do projeto  

---

## Tecnologias

- **C#**  
- **ASP.NET Core 8**  
- **Entity Framework Core**  
- **SQL Server**  
- **BCrypt** (hash de senha)  
- **JWT** (JSON Web Tokens)

---

## Execução local

Requisitos: SDK .NET 8 e SQL Server local.

**Segurança:** não coloque chaves JWT, senhas ou tokens em arquivos versionados. Para desenvolvimento, use o Secret Manager do .NET:

```bash
cd AutenticaNET.API
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "<chave-local-aleatoria-com-no-minimo-32-bytes>"
```

Substitua o exemplo por uma chave forte gerada localmente. Como alternativa, configure a variável de ambiente `Jwt__Key`. A inicialização falha de forma explícita quando a chave não estiver configurada ou tiver menos de 32 bytes.

Ajuste `ConnectionStrings:DefaultConnection` conforme o SQL Server local (preferencialmente via User Secrets). Depois aplique as migrations com `dotnet ef database update` (requer `dotnet-ef`) e execute `dotnet run`.

Endpoints:
- `POST /api/Auth/register` — cadastro.
- `POST /api/Auth/login` — login e geração do JWT.
- `GET /api/Users/profile` — perfil autenticado com Bearer token.

Projeto de estudo, não uma solução de autenticação pronta para produção.
