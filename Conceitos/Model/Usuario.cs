using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conceitos.Model
{
    public class Usuario : IdentityUser
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Sobrenome { get; set; } = string.Empty;
        [Required]
        public string Sexo { get; set; } = string.Empty;
        [NotMapped]
        public string Senha { get; set; } = string.Empty;
        [Required]
        public DateTime? DataNascimento { get; set; }
        [Required]
        public string Estado { get; set; } = string.Empty;
        [Required]
        public string Cidade { get; set; } = string.Empty;
        [Required]
        public string CEP { get; set; } = string.Empty;
        [Required]
        public string Endereco { get; set; } = string.Empty;
        [Required]
        public string Bairro { get; set; } = string.Empty;
        [Required]
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;

    }
}
