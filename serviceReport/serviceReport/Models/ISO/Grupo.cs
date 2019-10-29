using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serviceReport.Models.ISO
{
    public class Grupo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreGrupo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int Consecutivo { get; set; }

        public int IdDominio { get; set; }

        [ForeignKey("IdAnexo")]
        public Anexo Anexo { get; set; }

        public int IdAnexo { get; set; }

        public ICollection<Pregunta> Preguntas { get; set; }
    }
}