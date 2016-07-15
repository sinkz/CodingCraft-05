using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraft_05.Models
{
    [Table("Arquivos")]
    public class Arquivo : IArquivo
    {
        public Guid ArquivoId { get; set; }
        public Guid DiretorioId { get; set; }

        [Required]
        public String Nome { get; set; }
        [Required]
        public String MimeType { get; set; }

        public virtual Diretorio Diretorio { get; set; }
    }
}