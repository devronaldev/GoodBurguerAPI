# 🍔 Good Burguer API - Teste Técnico (Back-End)

Este projeto é uma solução para o desafio técnico proposto pela empresa **STgenetics Brasil**, 
com foco no desenvolvimento de uma API para gestão de pedidos de lanches.

---

## ✅ Sobre o projeto

A proposta consiste em construir uma **Web API em C# com .NET 8**, capaz de:

- Listar os lanches e os itens extras;
- Enviar, consultar, atualizar e remover pedidos;
- Calcular automaticamente os descontos com base nas combinações dos itens;
- Operar com o banco de dados **SQLite** (sem persistência externa).

---

## 🚀 Como executar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/devronaldev/good-burguer-api.git
cd good-burguer-api
```

### 2. Instalar as dependências

Certifique-se que o .NET SDK 8.0.0 esteja instalado na máquina.

```bash
dotnet --version
```

Tendo a versão devidamente instalada você apenas precisa restaurar as dependências.

```bash
dotnet restore
```

### Rodar a aplicação com hot reload (Swagger incluso)

```bash
dotnet watch run
```

A API estará disponível em:
```bash
http://localhost:5220
http://localhost:5220/swagger - Para acesso prático
```

## 🧩 Funcionalidades

- 🍔 Listar todos os lanches e extras
- 🥪 Listar apenas lanches
- 🍟 Listar apenas extras
- 📝 Criar novo pedido
- 💸 Calcular valor com descontos aplicados
- 📋 Listar todos os pedidos
- ✏️ Atualizar pedido existente
- 🗑️ Excluir pedido  

## 🛠️ Tecnologias utilizadas
- .NET 8
- C# ASP.NET Core Web API
- Entity Framework Core (SQLite)
- Swagger com Swashbuckle.AspNetCore

## 📬 Observações

> Este projeto foi desenvolvido exclusivamente para fins de **avaliação técnica** num **processo seletivo**.  
> Não há autenticação de usuários nem persistência em banco de dados externo.

## 👨‍💻 Desenvolvedor

**Ronald Pereira Evangelista**  
**Estudante de Análise e Desenvolvimento de Sistemas**  
**Desenvolvedor Back-end com foco em C# .NET**