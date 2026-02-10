using System;

namespace SistemaAlunos.Models {
    public class Aluno {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Curso { get; set; }
        public string CPF { get; set; }
    }
}
