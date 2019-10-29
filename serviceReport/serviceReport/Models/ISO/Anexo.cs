using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serviceReport.Models.ISO
{
    public class Anexo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int Consecutivo { get; set; }

        public bool Activo { get; set; }

        [ForeignKey("IdDominio")]
        public Dominio Dominio { get; set; }

        public int IdDominio { get; set; }

        public ICollection<Grupo> Grupos { get; set; }
    }
}