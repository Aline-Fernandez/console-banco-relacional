using System;
using System.Collections.Generic;
using SistemaAlunos.Models;
using SistemaAlunos.Repositories;

namespace SistemaAlunos {
    class Program {
        static void Main(string[] args) {
            AlunoRepository alunoRepository = new AlunoRepository();
            int opcao;

            do {
                Console.Clear(); // Limpa o console a cada iteração do menu
                Console.WriteLine("\n--- Sistema de Gestão de Alunos ---");
                Console.WriteLine("1. Listar Alunos");
                Console.WriteLine("2. Adicionar Novo Aluno");
                Console.WriteLine("3. Atualizar Aluno");
                Console.WriteLine("4. Excluir Aluno");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao)) {
                    switch (opcao) {
                        case 1:
                            ListarAlunos(alunoRepository);
                            break;
                        case 2:
                            AdicionarAluno(alunoRepository);
                            break;
                        case 3:
                            AtualizarAluno(alunoRepository);
                            break;
                        case 4:
                            ExcluirAluno(alunoRepository);
                            break;
                        case 5:
                            Console.WriteLine("Saindo do sistema. Até mais!");
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }
                }
                else {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número.");
                }

                if (opcao != 5) // Não pausa se a opção for sair
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 5);
        }

        static void ListarAlunos(AlunoRepository repository) {
            Console.WriteLine("\n--- Lista de Alunos ---");
            try {
                List<Aluno> alunos = repository.ListarTodos();

                if (alunos.Count == 0) {
                    Console.WriteLine("Nenhum aluno cadastrado.");
                    return;
                }

                foreach (var aluno in alunos) {

                    Console.WriteLine($"ID: {aluno.Id}, Nome: {aluno.Nome}, Data Nasc.: {aluno.DataNascimento:dd/MM/yyyy}, Curso: {aluno.Curso}, CPF: {aluno.CPF}");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao listar alunos: {ex.Message}");
                Console.WriteLine("Detalhes do erro: " + ex.ToString());
            }
        }

        static void AdicionarAluno(AlunoRepository repository) {
            Console.WriteLine("\n--- Adicionar Novo Aluno ---");
            Aluno novoAluno = new Aluno();

            Console.Write("Nome: ");
            novoAluno.Nome = Console.ReadLine();

            Console.Write("Data de Nascimento (DD/MM/AAAA): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNascimento)) {
                novoAluno.DataNascimento = dataNascimento;
            }
            else {
                Console.WriteLine("Formato de data inválido. Usando data padrão (hoje).");
                novoAluno.DataNascimento = DateTime.Today;
            }

            Console.Write("Curso: ");
            novoAluno.Curso = Console.ReadLine();

            Console.Write("CPF: ");
            novoAluno.CPF = Console.ReadLine();

            try {
                // Verifica se o CPF já exixte
                if (repository.ExisteCPF(novoAluno.CPF)) {
                    Console.WriteLine($"Erro: Já existe um aluno cadastrado com o CPF '{novoAluno.CPF}'.");
                    Console.WriteLine("Cadastro cancelado.");
                }
                else {
                    repository.Adicionar(novoAluno);
                    Console.WriteLine("Aluno adicionado com sucesso!");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao adicionar aluno: {ex.Message}");
                Console.WriteLine("Detalhes do erro: " + ex.ToString());
            }
        }

        static void AtualizarAluno(AlunoRepository repository) {
            Console.WriteLine("\n--- Atualizar Aluno ---");
            Console.Write("Digite o ID do aluno que deseja atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int idParaAtualizar)) {
                // O ideal aqui seria buscar o aluno pelo ID primeiro para preencher os dados atuais
                // Mas para simplificar, vamos pedir todos os dados novamente

                Aluno alunoAtualizado = new Aluno { Id = idParaAtualizar };

                Console.Write("Novo Nome: ");
                alunoAtualizado.Nome = Console.ReadLine();

                Console.Write("Nova Data de Nascimento (DD/MM/AAAA): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime novaDataNascimento)) {
                    alunoAtualizado.DataNascimento = novaDataNascimento;
                }
                else {
                    Console.WriteLine("Formato de data inválido. Usando data padrão (hoje).");
                    alunoAtualizado.DataNascimento = DateTime.Today;
                }

                Console.Write("Novo Curso: ");
                alunoAtualizado.Curso = Console.ReadLine();

                try {
                    repository.Atualizar(alunoAtualizado);
                    Console.WriteLine($"Aluno com ID {idParaAtualizar} atualizado com sucesso!");
                }
                catch (Exception ex) {
                    Console.WriteLine($"Erro ao atualizar aluno: {ex.Message}");
                    Console.WriteLine("Detalhes do erro: " + ex.ToString());
                }
            }
            else {
                Console.WriteLine("ID inválido.");
            }
        }

        static void ExcluirAluno(AlunoRepository repository) {
            Console.WriteLine("\n--- Excluir Aluno ---");
            Console.Write("Digite o ID do aluno que deseja excluir: ");
            if (int.TryParse(Console.ReadLine(), out int idParaExcluir)) {
                try {
                    // Buscar o aluno pelo ID para confirmar as informações antes de excluir
                    Aluno alunoParaExcluir = repository.BuscarPorId(idParaExcluir);

                    if (alunoParaExcluir == null) {// Verifica se o aluno foi encontrado
                        Console.WriteLine($"Aluno com ID {idParaExcluir} não encontrado.");
                        return; // Sai do método se o aluno não for encontrado
                    }

                    // Exibir as informações do aluno antes de pedir confirmação de exclusão
                    Console.WriteLine("\nInformações do Aluno a ser excluído:");
                    Console.WriteLine($"ID: {alunoParaExcluir.Id}");
                    Console.WriteLine($"Nome: {alunoParaExcluir.Nome}");
                    Console.WriteLine($"Data Nasc.: {alunoParaExcluir.DataNascimento}");
                    Console.WriteLine($"Curso: {alunoParaExcluir.Curso}");
                    Console.WriteLine($"CPF: {alunoParaExcluir.CPF}");
                    Console.WriteLine("_______________________________");


                    Console.Write($"Tem certeza que deseja excluir este aluno? (S/N): ");
                    string confirmacao = Console.ReadLine().ToUpper();

                    if (confirmacao == "S") {

                        repository.Excluir(idParaExcluir);
                        Console.WriteLine($"Aluno com ID {idParaExcluir} excluído com sucesso!");
                    }
                    else {
                        Console.WriteLine("Exclusão cancelada.");
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"Erro ao excluir aluno: {ex.Message}");
                    Console.WriteLine("Detalhes do erro: " + ex.ToString());
                }
            }
            else {
                Console.WriteLine("ID inválido.");
            }
        }
    }
}