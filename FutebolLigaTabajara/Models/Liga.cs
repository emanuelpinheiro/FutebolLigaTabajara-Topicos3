using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FutebolLigaTabajara.Models
{
    public class Liga
    {
        public int LigaId { get; set; }
        public string Nome { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<Time> Times { get; set; }
    }

}