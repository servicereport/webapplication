using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace serviceReport.Models.Auditoria
{
    public class EstadoAuditoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Estado { get; set; }
    }
}