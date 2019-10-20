using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace serviceReport.Models.ISO
{
    public class NivelCalificacion
    {
        [Key]
        public int Id { get; set; }

        public string NombreNivel { get; set; }

        public string Porcentaje { get; set; }

        public string Criterio { get; set; }
    }
}