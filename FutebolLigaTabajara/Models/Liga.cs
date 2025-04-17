using FutebolLigaTabajara.Models;
using System.Collections.Generic;
using System.Linq;

public class Liga
{
    public int LigaId { get; set; }

    public string Nome { get; set; }

    public bool Status { get; set; } // Liga apta para iniciar ou não

    public virtual ICollection<Time> Times { get; set; }

    // Verifica se a liga pode iniciar
    public bool PodeIniciar()
    {
        return Times != null &&
               Times.Count == 20 &&
               Times.All(t => t.Status && t.EstaApto());
    }
}
