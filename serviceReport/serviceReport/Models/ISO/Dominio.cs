using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace serviceReport.Models.ISO
{
    public class Dominio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreDominio { get; set; }

        [Required]
        public string Identificador { get; set; }

        [Required]
        public int Consecutivo { get; set; }

        public ICollection<Anexo> Anexos { get; set; }

    }
}