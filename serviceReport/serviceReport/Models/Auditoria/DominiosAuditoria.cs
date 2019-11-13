using serviceReport.Models.ISO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serviceReport.Models.Auditoria
{
    public class DominiosAuditoria
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdDominio")]
        public Dominio Dominio { get; set; }

        [Required]
        public int IdDominio { get; set; }

        [ForeignKey("IdAuditoria")]
        public Auditoria Auditoria { get; set; }

        [Required]
        public int IdAuditoria { get; set; }


        [NotMapped]
        public bool Asociado { get; set; }

        [NotMapped]
        public List<Pregunta> Preguntas { get; set; }
    }
}