using System;
using System.Collections.Generic;

namespace CodingCraft_05.Models
{
    public interface IDiretorio
    {
        Guid DiretorioId { get; set; }
        Diretorio DiretorioPai { get; set; }
        Guid? DiretorioPaiId { get; set; }
        string Nome { get; set; }
    }
}