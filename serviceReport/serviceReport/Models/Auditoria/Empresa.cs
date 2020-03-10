using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace serviceReport.Models.Auditoria
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreEntidad { get; set; }

        [Required]
        public string NombreContactoEntidad { get; set; }

        [Required]
        public string EmailEmpresa { get; set; }

        public string UrlLogo { get; set; }

        public bool Activo { get; set; }

        public List<Auditoria> Auditorias { get; set; }
    }
}