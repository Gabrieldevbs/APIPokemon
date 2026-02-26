📘 APIPokemon — Documentação Técnica
📌 Visão Geral

A APIPokemon é uma Web API desenvolvida em ASP.NET Core, estruturada com os princípios de Clean Architecture, permitindo o gerenciamento de Pokémons e seleção de favoritos por usuários autenticados.

O projeto foi desenvolvido com foco em:
Separação de responsabilidades
Escalabilidade
Testabilidade
Manutenibilidade
Boas práticas de arquitetura

🏗 Arquitetura

O projeto segue os princípios da Clean Architecture, organizando o sistema em camadas independentes:

APIPokemon
│
├── Domain
├── Application
├── Infrastructure
└── API (Presentation)
🔹 Domain

Responsável pelas regras de negócio centrais.

Contém:
Entidades (ex: Pokemon, User, Favorite)
Interfaces de repositório
Regras de domínio

Características:

Não depende de frameworks
Não conhece banco de dados
Não conhece ASP.NET Core

🔹 Application

Responsável pela orquestração dos casos de uso.

Contém:
Services / Use Cases
DTOs
Interfaces de comunicação

Função:

Executar regras do Domain
Coordenar chamadas para repositórios
Controlar fluxo da aplicação

🔹 Infrastructure

Responsável pela implementação técnica.

Contém:

Entity Framework Core
DbContext
Implementação dos Repositórios
Configuração do banco de dados

Função:

Persistência de dados
Comunicação com banco PostgreSQL

🔹 API (Presentation Layer)

Responsável pela exposição da aplicação via HTTP.

Contém:

Controllers
Configuração do Swagger
Configuração do JWT
Injeção de dependência

Função:

Receber requisições
Validar dados
Acionar casos de uso

🧠 Princípios Aplicados

SOLID

Inversão de Dependência
Separação de Responsabilidades
Arquitetura em Camadas
DTO Pattern
Repository Pattern
Injeção de Dependência

🔐 Autenticação

A API utiliza JWT (JSON Web Token) para autenticação.

Fluxo:

Usuário realiza login
Token JWT é gerado
Token deve ser enviado no Header:
Authorization: Bearer {token}
Rotas protegidas exigem autenticação válida.

🗄 Banco de Dados

Banco utilizado:

PostgreSQL

ORM:

Entity Framework Core

Migrations gerenciadas via:

dotnet ef migrations add InitialCreate
dotnet ef database update

📡 Endpoints Principais:
🔹 Autenticação
Método	Rota	Descrição
POST	/login	Autenticação do usuário
POST	/register	Registro de usuário
🔹 Pokémon
Método	Rota	Descrição
GET	/pokemon	Lista todos os Pokémons
GET	/pokemon/{id}	Retorna Pokémon específico
POST	/pokemon	Cria novo Pokémon
PUT	/pokemon/{id}	Atualiza Pokémon
DELETE	/pokemon/{id}	Remove Pokémon
🔹 Favoritos
Método	Rota	Descrição
POST	/favorites/{pokemonId}	Marca Pokémon como favorito
GET	/favorites	Lista favoritos do usuário

🔄 Fluxo de Requisição

Request chega no Controller
Controller chama um Use Case na Application
Application executa regras do Domain
Infrastructure persiste ou consulta dados
Resposta retorna ao cliente

🚀 Tecnologias Utilizadas

C#
.NET 10
ASP.NET Core
Entity Framework Core
PostgreSQL
JWT Authentication
Swagger
React (Frontend - em desenvolvimento)

🧪 Melhorias Futuras

Deploy em ambiente cloud
Utilização de IA para sugestões de favoritos
Criar uma nova função para que os users consigam fazer times de 6 pokemons.

Logs estruturados

Integração com IA para recomendação de Pokémon
