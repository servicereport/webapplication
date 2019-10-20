﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace serviceReport.Models.ISO
{
    public class Roll
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RollName { get; set; }
    }
}