using Microsoft.Data.SqlClient; // Importa o namespace necessário para trabalhar com SQL Server

namespace SistemaAlunos.Data {
    public class Conexao {

        private static string connectionString = "Server=tcp:servidor-aline-dev-2025.database.windows.net,1433;Initial Catalog=SistemaAlunoDB;Persist Security Info=False;User ID=admin.aline;Password=azure@Wilson05;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection ObterConexao() {
            //Retorna um novo objeto SqlConnection usando a string de conexão
            return new SqlConnection(connectionString);
        }
    }
}
