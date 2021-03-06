﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDosLivros.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public override string ToString()
        {
            return $"Categoria: {Name}";
        }
    }
}
