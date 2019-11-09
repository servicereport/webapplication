using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public bool Activo { get; set; }

        public ICollection<Anexo> Anexos { get; set; }

        [NotMapped]
        public string NombreCompleto { get { return $"{Identificador}.{Consecutivo} {NombreDominio}"; } }



    }
}