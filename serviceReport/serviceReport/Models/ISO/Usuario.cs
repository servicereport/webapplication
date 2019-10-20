using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serviceReport.Models.ISO
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [ForeignKey("IdRoll")]
        public Roll RollUsuario { get; set; }

        [Required]
        public int IdRoll { get; set; }
    }
}