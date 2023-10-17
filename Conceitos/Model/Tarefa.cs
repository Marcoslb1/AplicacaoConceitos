using Conceitos.Enum;
using System;

namespace Conceitos.Model
{
    public class Tarefa
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Previsao { get; set; }
        public Usuario Usuario { get; set; }
        public Situacao Situacao { get; set; }

    }
}
