using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrud.Model
{
    public partial class Person
    {
        [Key]
        public long P_ID { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(200)")]
        public string P_Firstname { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(200)")]
        public string P_Lastname { get; set; }
    }
}
