using serviceReport.Models.ISO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serviceReport.Models.Auditoria
{
    public class Auditoria
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdEmpresa")]
        public Empresa EmpresaAuditoria { get; set; }

        public int IdEmpresa { get; set; }

        [ForeignKey("IdAuditor")]
        public Usuario Auditor { get; set; }

        [Required]
        public int IdAuditor { get; set; }

        public DateTime FechaCreacion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaAuditoria { get; set; }

        [ForeignKey("IdEstadoAuditoria")]
        public EstadoAuditoria Estado { get; set; }

        [Required]
        public int IdEstadoAuditoria { get; set; }

        public List<DominiosAuditoria> Dominios { get; set; }        

    }
}