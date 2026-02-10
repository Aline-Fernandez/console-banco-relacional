using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; // Importa o namespace necessário para trabalhar com SQL Server
using SistemaAlunos.Data; // Para acessar a classe Conexao
using SistemaAlunos.Models; // Para acessar a classe Aluno

namespace SistemaAlunos.Repositories {
    public class AlunoRepository {
        // Método para listar todos os alunos do banco de dados
        public List<Aluno> ListarTodos() {
            List<Aluno> alunos = new List<Aluno>();
            // Usando 'using' garante que a conexão será fechada e descartada corretamente
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Comando SQL para selecionar todos os alunos, incluindo o CPF
                string query = "SELECT Id, Nome, DataNascimento, Curso, CPF FROM dbo.Alunos";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    conexao.Open(); // Abre a conexão com o banco de dados
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        // Lê cada linha retornada pela consulta
                        while (reader.Read()) {
                            Aluno aluno = new Aluno {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                DataNascimento = (DateTime)reader["DataNascimento"],
                                Curso = reader["Curso"].ToString(),
                                CPF = reader["CPF"].ToString() // Lê o CPF
                            };
                            alunos.Add(aluno); // Adiciona o aluno à lista
                        }
                    }
                }
            }
            return alunos; // Retorna a lista de alunos
        }

        // Método para buscar um aluno pelo seu ID
        public Aluno BuscarPorId(int id) {
            Aluno aluno = null; // Inicializa como null. Se não encontrar, retorna null.
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Seleciona todas as colunas para o aluno com o ID especificado
                string query = "SELECT Id, Nome, DataNascimento, Curso, CPF FROM dbo.Alunos WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    comando.Parameters.AddWithValue("@Id", id); // Adiciona o ID como parâmetro

                    conexao.Open();
                    using (SqlDataReader leitor = comando.ExecuteReader()) {
                        // Se encontrar um registro, lê os dados e cria o objeto Aluno
                        if (leitor.Read()) {
                            aluno = new Aluno {
                                Id = (int)leitor["Id"],
                                Nome = leitor["Nome"].ToString(),
                                DataNascimento = (DateTime)leitor["DataNascimento"],
                                Curso = leitor["Curso"].ToString(),
                                CPF = leitor["CPF"].ToString()
                            };
                        }
                    }
                }
            }
            return aluno; // Retorna o aluno encontrado ou null se não encontrar
        }

        // Método para adicionar um novo aluno ao banco de dados
        public void Adicionar(Aluno aluno) {
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Comando SQL para inserir um novo aluno, incluindo o CPF.
                // Usamos parâmetros (@Nome, @DataNascimento, @Curso, @CPF) para segurança.
                string query = "INSERT INTO dbo.Alunos (Nome, DataNascimento, Curso, CPF) VALUES (@Nome, @DataNascimento, @Curso, @CPF)";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    // Adiciona os valores do objeto Aluno como parâmetros do comando SQL
                    comando.Parameters.AddWithValue("@Nome", aluno.Nome);
                    comando.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
                    comando.Parameters.AddWithValue("@Curso", aluno.Curso);
                    comando.Parameters.AddWithValue("@CPF", aluno.CPF); // Adiciona o parâmetro CPF

                    conexao.Open(); // Abre a conexão
                    comando.ExecuteNonQuery(); // Executa o comando de inserção
                }
            }
        }

        public bool ExisteCPF(string cpf) {
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Seleciona o Id de um aluno onde o CPF corresponde.
                // Usamos COUNT(*) ou apenas SELECT Id para verificar a existência.
                String query = "SELECT COUNT(*) FROM dbo.Alunos WHERE CPF = @CPF";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    comando.Parameters.AddWithValue("@CPF", cpf); // Adiciona CPF como parametro

                    conexao.Open();
                    // ExecuteScaler retorna o primeiro valor da primeira linha do resultado da query
                    int count = (int)comando.ExecuteScalar();
                    return count > 0; // Retorna true se o counnt for maior que 0 (CPF existe)
                }
            }
        }

        // Método para atualizar um aluno existente no banco de dados
        public void Atualizar(Aluno aluno) {
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Comando SQL para atualizar um aluno.
                // Atualizamos Nome, DataNascimento, Curso E CPF.
                // A cláusula WHERE Id = @Id é CRUCIAL para atualizar o aluno correto.
                string query = "UPDATE dbo.Alunos SET Nome = @Nome, DataNascimento = @DataNascimento, Curso = @Curso, CPF = @CPF WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    // Adiciona os valores do objeto Aluno como parâmetros do comando SQL
                    comando.Parameters.AddWithValue("@Nome", aluno.Nome);
                    comando.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
                    comando.Parameters.AddWithValue("@Curso", aluno.Curso);
                    comando.Parameters.AddWithValue("@CPF", aluno.CPF); // Adiciona o parâmetro CPF
                    comando.Parameters.AddWithValue("@Id", aluno.Id); // Parâmetro para identificar qual aluno atualizar

                    conexao.Open(); // Abre a conexão
                    int linhasAfetadas = comando.ExecuteNonQuery(); // Executa o comando de atualização

                    if (linhasAfetadas == 0) {
                        // Opcional: Lançar uma exceção se o aluno não foi encontrado
                        throw new Exception($"Aluno com ID {aluno.Id} não encontrado para atualização.");
                    }
                }
            }
        }

        // Método para excluir um aluno do banco de dados pelo ID
        public void Excluir(int id) {
            using (SqlConnection conexao = Conexao.ObterConexao()) {
                // Comando SQL para excluir um aluno com base no 'Id'.
                string query = "DELETE FROM dbo.Alunos WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(query, conexao)) {
                    // Adiciona o Id do aluno como parâmetro
                    comando.Parameters.AddWithValue("@Id", id);

                    conexao.Open(); // Abre a conexão
                    int linhasAfetadas = comando.ExecuteNonQuery(); // Executa o comando de exclusão

                    if (linhasAfetadas == 0) {
                        // Opcional: Lançar uma exceção se o aluno não foi encontrado
                        throw new Exception($"Aluno com ID {id} não encontrado para exclusão.");
                    }
                }
            }
        }
    }
}
