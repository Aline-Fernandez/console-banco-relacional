<img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="C# Logo" width="80"/> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original.svg" alt=".NET Logo" width="80"/> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/azure/azure-original.svg" alt="Azure Logo" width="80"/>
<br>

# 📘 Sistema de Gestão de Alunos (C# + Azure SQL)

<img src="https://img.shields.io/badge/Status-Concluído-success?style=for-the-badge" alt="Status" /> <img src="https://img.shields.io/badge/Tecnologia-C%23%20.NET%208-512bd4?style=for-the-badge&logo=dotnet" alt="C# .NET" /> <img src="https://img.shields.io/badge/Cloud-Azure%20SQL-0089D6?style=for-the-badge&logo=microsoftazure" alt="Azure SQL" />



Este projeto é uma aplicação de console desenvolvida em **C# .NET 8** para gerenciar registros de alunos. O diferencial desta aplicação é a integração real com o **Azure SQL Database**, realizando operações de CRUD diretamente na nuvem.

---

## 🚀 Competências Demonstradas
- [x] **CRUD Completo:** Listagem, inserção, atualização e exclusão de registros.
- [x] **Integração Cloud:** Configuração e conexão com Azure SQL Database.
- [x] **Acesso a Dados:** Uso do `Microsoft.Data.SqlClient` para persistência robusta.
- [x] **Infraestrutura:** Configuração de Firewalls e Schemas de Banco de Dados via SSMS.

## 🛠️ Tecnologias Utilizadas
* **Linguagem:** C# (.NET 8).
* **Banco de Dados:** Azure SQL Database (Serverless).
* **Ferramentas:** Visual Studio 2022 e SQL Server Management Studio (SSMS).
* **Driver:** Microsoft.Data.SqlClient.

---

## ⚙️ Configuração e Instalação

### 1. Banco de Dados (Azure & SSMS)
* **Servidor:** Criado no Azure (Region: Brazil South).
* **Firewall:** Configurado para permitir o acesso do IP do cliente.
* **Schema:** Tabela `Alunos` criada via script SQL:
```sql
CREATE TABLE Alunos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    DataNascimento DATE,
    Curso VARCHAR(100)
);
