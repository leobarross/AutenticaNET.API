# AutenticaNET.API

Projeto de estudo em **C# / ASP.NET Core 8** para aprender autenticação em APIs REST com JWT, Entity Framework Core e SQL Server.

## Funcionalidades

- Registro de usuário com senha protegida usando BCrypt.
- Login com emissão de JWT assinado.
- Endpoint de perfil protegido por autenticação.
- Role padrão `User` incluída no token.
- Validação dos dados recebidos com Data Annotations.
- Respostas HTTP: 201 para cadastro, 409 para duplicidade, 401 para credenciais inválidas.
- Configuração JWT sem chave publicada no repositório.

> Projeto educacional: não deve ser considerado um provedor de identidade pronto para produção. Recursos como proteção contra tentativas repetidas de login e gestão de sessões não estão implementados.

## Tecnologias

.NET 8, ASP.NET Core, Entity Framework Core, SQL Server, JWT, BCrypt e Swagger (em desenvolvimento).

## Executar localmente

Requisitos: .NET SDK 8, SQL Server e ferramenta `dotnet-ef`.

1. Entre na pasta `AutenticaNET.API` (onde está o arquivo `.csproj`).
2. Configure uma chave JWT **aleatória e exclusiva**, com no mínimo 32 bytes, por User Secrets:

```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "SUBSTITUA_POR_CHAVE_ALEATORIA_FORTE_COM_32_BYTES_OU_MAIS"
```

A chave acima é apenas um exemplo e **não** deve ser reutilizada. Também é possível usar a variável de ambiente `Jwt__Key`. Caso seja necessário, ajuste `ConnectionStrings:DefaultConnection` para seu SQL Server local via User Secrets.

3. Execute:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

O Swagger fica disponível em `/swagger` somente em ambiente de desenvolvimento.

### Exemplos de requisição

`POST /api/Auth/register`

```json
{"username":"usuario_teste","email":"usuario@example.com","password":"SenhaForteExemplo123"}
```

`POST /api/Auth/login`

```json
{"email":"usuario@example.com","password":"SenhaForteExemplo123"}
```

`GET /api/Users/profile`: envie `Authorization: Bearer <token>`.

## Testes

Os testes ficam em `tests/AutenticaNET.API.Tests`. Execute na raiz:

```bash
dotnet test tests/AutenticaNET.API.Tests/AutenticaNET.API.Tests.csproj
```

O GitHub Actions executa build e testes a cada push e pull request, caso Actions esteja habilitado para o repositório.

## Observações de segurança

A chave de assinatura JWT que anteriormente estava versionada foi removida do arquivo atual, mas **permanece no histórico Git**. Se essa chave foi utilizada em algum ambiente real, substitua-a e invalide os tokens correspondentes. Nunca publique segredos ou dados reais de usuários.

Não são necessários certificados ou credenciais reais para estudar este projeto.
