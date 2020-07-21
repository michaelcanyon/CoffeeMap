﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models.Intermediary_models
{
    public class Coffee_Index : Entity
    {
        [Required]
        public int Cofee_NodeId { get; set; }

        [Required]
        public int IndexId { get; set; }
    }
}
