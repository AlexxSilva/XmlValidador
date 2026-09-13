# XMLValidador

Sistema desenvolvido em **C# e .NET 8** para importação, análise e validação de XMLs fiscais, utilizando regras de negócio independentes e uma arquitetura organizada em camadas.

O projeto foi desenvolvido com foco em **Engenharia de Software**, aplicando conceitos como Clean Architecture, SOLID, Programação Orientada a Objetos, Injeção de Dependência, testes automatizados e separação de responsabilidades.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12-239120?style=flat-square&logo=csharp)
![xUnit](https://img.shields.io/badge/Tests-xUnit-red?style=flat-square)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat-square&logo=microsoftsqlserver)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=flat-square)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=flat-square&logo=swagger)
![Status](https://img.shields.io/badge/Status-Concluído-success?style=flat-square)

---

## 📌 Sobre o projeto

O **XMLValidador** é uma aplicação desenvolvida para importar e analisar XMLs fiscais, identificando inconsistências por meio de regras de negócio específicas.

O sistema recebe um XML, realiza o processamento das informações e executa diferentes validações, apresentando os resultados encontrados.

O projeto foi construído de forma incremental como um laboratório prático de desenvolvimento de software, permitindo aplicar conceitos de arquitetura, orientação a objetos, testes, banco de dados e desenvolvimento de APIs.

---

## 🎯 Funcionalidades

Atualmente o projeto possui:

- 📄 Importação de XML fiscal
- 🔎 Análise e processamento do XML
- ✅ Validação através de regras de negócio
- 📋 Identificação de erros e inconsistências
- 💾 Persistência das notas fiscais
- 📝 Histórico das validações realizadas
- 🌐 API REST para processamento dos XMLs
- 📖 Documentação da API através do Swagger / OpenAPI
- 🖥️ Interface Web para importação dos XMLs
- 🧪 Testes automatizados das regras e casos de uso

---

## 🏗️ Arquitetura

O projeto utiliza uma organização baseada em **Clean Architecture**, separando as responsabilidades da aplicação.

```text
XMLValidador
│
├── XmlValidador.Domain
│   ├── Entities
│   ├── ValueObjects
│   └── Exceptions
│
├── XmlValidador.Application
│   ├── Interfaces
│   ├── UseCases
│   └── ValidacoesXml
│
├── XmlValidador.Infrastructure
│   ├── Persistence
│   ├── Repositories
│   └── Configurations
│
├── XmlValidador.Api
│   └── Endpoints
│
├── XmlValidador.Web
│   └── Interface Web
│
└── XmlValidador.Tests
    └── Testes automatizados
