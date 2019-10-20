using serviceReport.Models.ISO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serviceReport.Models.Auditoria
{
    public class AuditoriaRespuestas
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdPregunta")]
        public Pregunta Pregunta { get; set; }

        [Required]
        public int IdPregunta { get; set; }

        [ForeignKey("IdCalificacion")]
        public NivelCalificacion Calificacion { get; set; }

        [Required]
        public int IdCalificacion { get; set; }
    }
}