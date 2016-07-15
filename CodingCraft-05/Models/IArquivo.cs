using System;

namespace CodingCraft_05.Models
{
    public interface IArquivo
    {
        Guid ArquivoId { get; set; }
        Guid DiretorioId { get; set; }
        string MimeType { get; set; }
        string Nome { get; set; }
    }
}