using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FutebolLigaTabajara.Models
{
    public class ComissaoTecnica
    {
        public int ComissaoTecnicaId { get; set; }

        [Required]
        public string Nome { get; set; }

        public Cargo Cargo { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public int TimeId { get; set; }
        public virtual Time Time { get; set; }
    }

}