using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReferenceOfPerson.Core.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }

        public int PersonId { get; set; }

        [Required]
        [EnumDataType(typeof(NumberType))]
        public  NumberType? Type { get; set; }


    }
}