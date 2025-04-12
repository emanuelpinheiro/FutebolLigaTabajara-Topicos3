﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FutebolLigaTabajara.Models
{
    public class Jogador
    {
        public int JogadorId { get; set; }

        [Required]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public string Nacionalidade { get; set; }

        public Posicao Posicao { get; set; }

        public int NumeroCamisa { get; set; }

        public double Altura { get; set; }

        public double Peso { get; set; }

        public PePreferido PePreferido { get; set; }

        public int TimeId { get; set; }
        public virtual Time Time { get; set; }
    }

}