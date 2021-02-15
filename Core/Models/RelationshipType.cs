using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReferenceOfPerson.Core.Models
{
    public enum  RelationshipType :byte
    {
        Colegue = 1,
        Familiar = 2,
        Relative = 3,
        Other = 4
    }

   
    
}