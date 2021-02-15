using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ReferenceOfPerson.Core.Utilities
{
    public class EngOrGeoLetters : ValidationAttribute
    {
        private const string EngExp = @"^[A-z]+$";
        private const string GeoExp = @"^[ა-ჰ]+$";

        public override bool IsValid(object value)
        {
            //if (!value == null)
            //{
            //    return (String.IsNullOrWhiteSpace(str) || Regex.IsMatch(str, EngExp) || Regex.IsMatch(str, GeoExp));
            //}

            string str = (value == null) ? string.Empty : value.ToString();
            return (String.IsNullOrEmpty(str) || Regex.IsMatch(str, EngExp) || Regex.IsMatch(str, GeoExp));

        }
    }
}