using System;

namespace TesteHelena.Models
{
    public class Empresa
    {
        // Propriedades
        public int Id { get; set; }
        public string? AvatarUrl { get;  private set; }
        public string? NomeFantasia { get; set; }
        public string? RazaoSocial { get; set; }
        public int QuantidadeDeFuncionarios { get; set; }
        public bool Active { get; set; } = true;

        // Criando os construtores
        public Empresa(int id, string avatarUrl, string nomeFantasia, string razaoSocial, int quantidadeDeFuncionarios, bool active)
        {
            Id = id;
            AvatarUrl = avatarUrl;
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            QuantidadeDeFuncionarios = quantidadeDeFuncionarios;
            Active = active;
        }
        public Empresa() { }
    }
}