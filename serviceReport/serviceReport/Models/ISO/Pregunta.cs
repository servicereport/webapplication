using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serviceReport.Models.ISO
{
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        [ForeignKey("IdGrupo")]
        public Grupo GrupoPregunta { get; set; }

        public int IdGrupo { get; set; }

        public bool Activo { get; set; }
    }
}