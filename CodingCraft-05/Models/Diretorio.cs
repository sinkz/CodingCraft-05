using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraft_05.Models
{
    [Table("Diretorios")]
    public class Diretorio : IDiretorio
    {
        [Key]
        public Guid DiretorioId { get; set; }
        public Guid? DiretorioPaiId { get; set; }

        [Required]
        public String Nome { get; set; }

        public virtual Diretorio DiretorioPai { get; set; }
        [JsonIgnore]
        public virtual ICollection<Arquivo> Arquivos { get; set; }
    }
}