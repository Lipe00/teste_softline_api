# Desafio Técnico SoftLine – Backend (.NET 8)
Este projeto é um desafio técnico da SoftLine, focado no desenvolvimento de um backend em .NET 8, com autenticação JWT, cadastro de usuários e operações CRUD para clientes e produtos.
O frontend foi desenvolvido separadamente com HTML, JS, jQuery e Bootstrap e se comunica com esta API via HTTP.

## Funcionalidades
  - Criar o primeiro usuário (/User/FirstAuth)
  - Login com autenticação JWT (/auth/login)
  - CRUD de Clientes (/Client)
  - CRUD de Produtos (/Product)
  - CRUD de Usuários (/User)
  >⚠️ A API exige autenticação JWT. Antes de qualquer operação, é necessário criar o primeiro usuário.

## Tecnologias Utilizadas
  - .NET 8
  - MySQL
  - JWT para autenticação
  - HTML, JS, jQuery e Bootstrap no frontend (repositório separado)

## Estrutura de Pastas
  ```plaintext
 Solução 'softline.API'
├── domain/                  # Camada de domínio: Entidades, Interfaces e Regras de Negócio
├── infraestrutura/          # Camada de infraestrutura: Acesso a dados, Repositórios e DbContext
└── softline.API/            # Projeto principal da API (Entry Point)
    ├── Controllers/         # Endpoints da API (Ex: AuthController, UserController, ClientController)
    ├── DTOs/                # Data Transfer Objects (objetos de requisição e resposta)
    ├── Services/            # Lógica de aplicação e serviços auxiliares
    │   └── TokenService.cs  # Geração e validação de tokens JWT
    ├── Properties/          # Configurações de inicialização (launchSettings.json)
    ├── appsettings.json     # Strings de conexão, JWT e outras variáveis de ambiente
    ├── Key.cs               # Chaves de criptografia e constantes de segurança
    ├── Program.cs           # Configuração do pipeline, middleware e injeção de dependências
    └── softline.API.http    # Arquivo para testes de requisições HTTP (ex: via REST Client)
```
## Como Executar
>Não é necessário criar o banco manualmente. As migrations da API criam as tabelas automaticamente.

1- Certifique-se de ter .NET 8 SDK e MySQL instalados.

2- Configure a connection string no appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=softline;User=<SEU_USUARIO_AQUI>;Password=<SUA_SENHA_AQUI>;"
  }
}
```
3- Execute a api

```bash
  dotnet run
```
4- A aplicação vai abrir o swagger em:

```plaintext
  https://localhost:7138/swagger
```

## Primeira Autenticação (FirstAuth)
Antes de usar qualquer rota protegida:

Abra /User/FirstAuth no Swagger.

Crie o primeiro usuário.

> Só será permitido se nenhum usuário existir no banco.

Depois, vá em /auth/login, informe usuário e senha e receba o token JWT.

Clique em “Authorize” no Swagger e insira:

```plaintext
  Bearer <SEU_TOKEN>
```
> Agora todas as rotas protegidas estarão acessíveis.

## Frontend
O frontend está localizado nesse [repositório](https://github.com/Lipe00/teste_softline_front)
