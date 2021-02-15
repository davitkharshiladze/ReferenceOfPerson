using System;
using System.ComponentModel.DataAnnotations;

namespace ReferenceOfPerson.Core.Models
{
    public class PersonQuery : QueryString
    {
        public Gender? Gender { get; set; }
        public uint MinYearOfBirth { get; set; }
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;

    }
}