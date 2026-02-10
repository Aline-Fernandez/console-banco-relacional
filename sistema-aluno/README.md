# Sistema de Gestão de Alunos

# 📘 SistemaAlunos

Este projeto é uma aplicação de console simples desenvolvida em C# .NET para gerenciar registros de alunos. Ele demonstra a integração de uma aplicação .NET com um banco de dados SQL Server hospedado no Azure SQL Database, realizando operações básicas de CRUD (Create, Read, Update, Delete).

## 🚀 Funcionalidades

-   **Listar Alunos:** Exibe todos os alunos cadastrados no banco de dados.
-   **Adicionar Novo Aluno:** Permite inserir novos registros de alunos no banco de dados, incluindo nome, data de nascimento e curso.
-   **Atualizar Aluno:** Permite modificar os dados de um aluno existente.
-   **Excluir Aluno:** Permite remover um registro de aluno do banco de dados.
-   **Conexão com Azure SQL Database:** Demonstra a configuração e o uso de uma string de conexão para interagir com um banco de dados remoto na nuvem.

## ✨ Tecnologias Utilizadas

-   **C#**: Linguagem de programação principal.
-   **.NET 8**: Framework de desenvolvimento da aplicação console.
-   **Azure SQL Database**: Serviço de banco de dados relacional na nuvem da Microsoft.
-   **SQL Server Management Studio (SSMS)**: Ferramenta para gerenciar e interagir com o banco de dados SQL.
-   **Microsoft.Data.SqlClient**: Pacote NuGet para conexão e operações com SQL Server em .NET.

## 📋 Pré-requisitos

Para rodar este projeto, você precisará ter o seguinte instalado:

-   **Visual Studio**: IDE com a carga de trabalho ".NET desktop development" (versão 2022 ou superior recomendada).
-   **.NET 8 SDK**: Kit de Desenvolvimento de Software para .NET 8.
-   **SQL Server Management Studio (SSMS)**: Para gerenciar o banco de dados.
-   **Conta Azure**: Com permissões para criar recursos (especificamente Azure SQL Database). Recomenda-se o uso da conta **Azure for Students** para recursos gratuitos ou de baixo custo.

## ⚙️ Configuração e Instalação

Siga os passos abaixo para configurar e rodar o projeto:

### 1. Configuração do Azure SQL Database

1.  **Crie um Servidor Azure SQL:**
    * No Portal do Azure, procure por "SQL Server" e crie um novo.
    * Nome do Servidor (ex: `servidor-dev-2025-db`).
    * Defina um **Login de administrador do servidor** (ex: `admin_db`) e uma **senha forte**. **Guarde essa senha!**
    * Escolha uma **Região** próxima a você (ex: `Brazil South`).
2.  **Crie um Banco de Dados SQL:**
    * Dentro do servidor SQL recém-criado, adicione um novo banco de dados.
    * Nome do Banco de Dados (ex: `MeuSistemaDB`).
    * **Importante**: Em "Nível de computação", selecione **"Sem Servidor" (Serverless)** para otimização de custos e auto-pausa.
    * Desabilite o "Microsoft Defender para SQL" e "Habilitar enclaves seguros" para evitar custos adicionais.
3.  **Configure o Firewall do Servidor:**
    * No seu servidor SQL no Azure, vá em "Rede".
    * Certifique-se de que "Permitir que serviços e recursos do Azure acessem este servidor" esteja **DESATIVADO**.
    * **Adicione seu endereço IP atual:** Clique em "Adicionar endereço IP do cliente atual" (`seu.ip.publico.aqui`) e clique em "Salvar". Isso permitirá que seu computador se conecte.
4.  **Obtenha a String de Conexão:**
    * No seu **Banco de Dados SQL** (`MeuSistemaDB`), vá em "Cadeias de conexão".
    * Copie a string da aba **"ADO.NET"**. Ela será semelhante a:
        `Server=tcp:servidor-dev-2025-db.database.windows.net,1433;Initial Catalog=MeuSistemaDB;Persist Security Info=False;User ID=admin_db;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;`

### 2. Configuração do Schema do Banco de Dados (via SSMS)

1.  **Conecte-se ao Azure SQL Database no SSMS:**
    * Abra o SSMS.
    * Clique em "Conectar" -> "Mecanismo de Banco de Dados".
    * Nome do Servidor: `servidor-dev-2025-db.database.windows.net`
    * Autenticação: `Autenticação do SQL Server`
    * Login: `admin_db`
    * Senha: `SuaSenhaForteAqui`
2.  **Crie a Tabela `Alunos`:**
    * No "Explorador de Objetos" do SSMS, **clique com o botão direito no seu banco de dados `MeuSistemaDB`** (NÃO no `master`!) e selecione "Nova Consulta".
    * Cole e execute o seguinte script SQL:
        ```sql
        CREATE TABLE Alunos (
            Id INT PRIMARY KEY IDENTITY(1,1),
            Nome VARCHAR(100) NOT NULL,
            DataNascimento DATE,
            Curso VARCHAR(100)
        );
        ```
    * **Verifique a tabela:** Expanda `MeuSistemaDB` -> `Tabelas` e confirme que `dbo.Alunos` está listado.

### 3. Configuração do Projeto C#

1.  **Clone o Repositório:**
    ```bash
    git clone https://github.com/Aline-Fernandez/SistemaAlunos.git
    ```
    (Ou baixe e descompacte o projeto)
2.  **Abra no Visual Studio:**
    * Abra o arquivo de solução `.sln` no Visual Studio.
3.  **Instale o Pacote NuGet:**
    * No Visual Studio, vá em `Ferramentas` > `Gerenciador de Pacotes NuGet` > `Gerenciar Pacotes NuGet para a Solução...`.
    * Na aba `Procurar`, pesquise por `Microsoft.Data.SqlClient` e instale a versão mais recente para o seu projeto `SistemaAlunos`.
4.  **Atualize a String de Conexão:**
    * Abra o arquivo `Data/Conexao.cs`.
    * Substitua `"SUA_STRING_DE_CONEXÃO_AQUI"` pela string de conexão completa que você obteve do Portal do Azure.
    * **Lembre-se de substituir `{your_password}` pela sua senha real.**
    * O arquivo deve ficar assim:
        ```csharp
        using Microsoft.Data.SqlClient; // Correto!

        namespace SistemaAlunos.Data
        {
            public class Conexao
            {
                private static string connectionString = "Server=tcp:servidor-dev-2025-db.database.windows.net,1433;Initial Catalog=MeuSistemaDB;Persist Security Info=False;User ID=admin_db;Password=[SUA_SENHA_AQUI];MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                public static SqlConnection ObterConexao()
                {
                    return new SqlConnection(connectionString);
                }
            }
        }
        ```
5.  **Verifique os Usings:**
    * Confirme que em `Conexao.cs`, `AlunoRepository.cs` e `Program.cs`, a linha de importação do SQL Client é `using Microsoft.Data.SqlClient;`.

## 💡 Uso da Aplicação

Siga as instruções no menu do console:

-   Digite `1` para listar os alunos cadastrados.
-   Digite `2` para adicionar um novo aluno (você será solicitado a fornecer Nome, Data de Nascimento e Curso).
-   Digite `3` para atualizar os dados de um aluno existente.
-   Digite `4` para excluir um aluno pelo ID.
-   Digite `5` para sair da aplicação.

## 🚧 Possíveis Problemas e Soluções Rápidas

-   **"Login failed for user 'admin_db'"**: Verifique seu nome de usuário (`admin_db`) e senha na string de conexão e no SSMS.
-   **"Cannot open server 'servidor-dev-2025-db' requested by the login"**: Problema de Firewall. Verifique se seu IP atual está adicionado nas regras de firewall do servidor no Azure.
-   **"Não está achando o objeto Alunos"**: Verifique se `Initial Catalog` na string de conexão aponta para o nome EXATO do seu banco de dados (`MeuSistemaDB`). Confirme também que a tabela `dbo.Alunos` realmente foi criada dentro de `MeuSistemaDB` (e não no banco de dados `master`!) no SSMS.
-   **`System.IndexOutOfRangeException: 'NomeDaColunaErrada'`**: Há um erro de digitação no nome de uma coluna que seu código está tentando ler/gravar. Renomeie a coluna no banco de dados para o nome correto ou ajuste a propriedade e leitura no C# (a primeira opção é a mais recomendada).

## ⏭️ Próximos Passos (Melhorias Futuras)

Este projeto serve como base. Você pode expandi-lo adicionando:

-   Tratamento de erros mais robusto e logging.
-   Uma interface de usuário (UI) mais amigável (ex: WPF, WinForms, ASP.NET Core).
-   Validação de entrada de dados mais completa.
-   Busca de aluno por ID ou nome.

## 🤝 Contribuindo

Se você deseja contribuir para este projeto, sinta-se à vontade para fazer um fork, implementar melhorias e enviar um Pull Request.

## 📄 Licença

Este projeto está licenciado sob a Licença MIT.

---
