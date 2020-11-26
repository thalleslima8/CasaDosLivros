using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDosLivros.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Preco Inválido")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Categoria Categoria { get; set; }

        public string Details()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Detalhes do Livro");
            stringBuilder.AppendLine("=================");
            stringBuilder.AppendLine($"Livro: {Title}");
            stringBuilder.AppendLine($"Autor: {Author}");
            stringBuilder.AppendLine($"Preço: {Price}");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
