# XMLValidador

> Sistema desenvolvido em .NET para validação e análise de XMLs fiscais, com foco em boas práticas de desenvolvimento, organização arquitetural e regras de negócio testáveis.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square\&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12-239120?style=flat-square\&logo=csharp)
![Tests](https://img.shields.io/badge/Tests-xUnit-red?style=flat-square)
![Status](https://img.shields.io/badge/Status-Em%20desenvolvimento-orange?style=flat-square)

---

## 📌 Sobre o projeto

O **XMLValidador** é um projeto criado para validar e analisar XMLs fiscais, identificando problemas estruturais e inconsistências através de regras de negócio.

O projeto está sendo desenvolvido de forma incremental, aplicando conceitos de **Engenharia de Software**, como:

* Clean Architecture
* SOLID
* Programação Orientada a Objetos
* Separação de responsabilidades
* Injeção de dependência
* Testes automatizados

A ideia é evoluir gradualmente de um validador de XML para uma solução mais completa de **análise de documentos fiscais**.

---

## 🏗️ Arquitetura

O projeto utiliza uma organização baseada em **Clean Architecture**:

```text
XMLValidador
│
├── Domain
│   ├── Entities
│   ├── ValueObjects
│   └── Exceptions
│
├── Application
│   ├── Interfaces
│   ├── UseCases
│   └── ValidacoesXml
│
├── Infrastructure
│
└── Tests
```

### Domain

Contém as entidades, objetos de valor e regras relacionadas ao domínio da aplicação.

### Application

Contém os casos de uso, interfaces e validações da aplicação.

### Infrastructure

Responsável por recursos externos, como banco de dados e outras implementações de infraestrutura.

### Tests

Contém os testes automatizados das regras e casos de uso.

---

## 🧪 Testes

O projeto utiliza **xUnit** para testes automatizados.

Os testes têm como objetivo garantir que as regras de negócio funcionem corretamente e permitir que novas funcionalidades sejam adicionadas com segurança.

Exemplo de fluxo:

```text
XML
 ↓
Parser
 ↓
ValidarXmlUseCase
 ↓
Validações
 ↓
Resultado
```

---

## 🛠️ Tecnologias

* C#
* .NET 8
* xUnit
* SQL Server
* Entity Framework Core
* Swagger / OpenAPI
* Git / GitHub

---

## 🗺️ Cronograma / Roadmap

O projeto será desenvolvido em etapas:

### ✅ Etapa 1 — Fundamentos

* [x] Estrutura do projeto
* [x] Clean Architecture
* [x] Domain
* [x] Application
* [x] Entidades e Value Objects
* [x] Casos de uso
* [x] Validações
* [x] Testes unitários

### 🔄 Etapa 2 — Banco de Dados

* [ ] Configuração do SQL Server
* [ ] Entity Framework Core
* [ ] Repositórios
* [ ] Persistência dos XMLs
* [ ] Histórico de validações

### ⏳ Etapa 3 — Validações Avançadas

* [ ] Novas regras fiscais
* [ ] Validação de valores
* [ ] Validação de totais
* [ ] Detecção de inconsistências

### ⏳ Etapa 4 — API

* [ ] API REST
* [ ] Upload de XML
* [ ] Endpoint de validação
* [ ] Consulta de resultados
* [ ] Swagger

### ⏳ Etapa 5 — Evolução

* [ ] Processamento em lote
* [ ] Relatórios
* [ ] Docker
* [ ] CI/CD
* [ ] Cloud / AWS

---

## 🚀 Como executar

### Pré-requisitos

* .NET 8 SDK
* Visual Studio ou VS Code
* SQL Server

### Clonar o projeto

```bash
git clone https://github.com/seu-usuario/XMLValidador.git
```

### Restaurar dependências

```bash
dotnet restore
```

### Compilar

```bash
dotnet build
```

### Executar os testes

```bash
dotnet test
```

---

## 🎯 Objetivo

O objetivo do XMLValidador é construir uma solução capaz não apenas de verificar se um XML é válido, mas também de identificar **problemas, inconsistências e possíveis erros fiscais**.

O projeto também serve como laboratório prático para evolução em:

**C# → .NET → Arquitetura → Testes → Banco de Dados → API → Cloud**

---

## 📈 Status

🚧 **Em desenvolvimento**

O projeto está sendo desenvolvido de forma incremental, com novas funcionalidades sendo adicionadas conforme cada etapa do roadmap é concluída.

---

## 👨‍💻 Autor

**Alex Moura**

Projeto desenvolvido para estudo, prática e evolução profissional em Engenharia de Software.
