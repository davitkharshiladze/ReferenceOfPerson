using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ReferenceOfPerson.Core.Models;

namespace ReferenceOfPerson.Controllers.Resources
{
    public class PhoneNumberResource
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }

        [Required]
        [EnumDataType(typeof(NumberType), ErrorMessage = "type of phone number is invalid")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NumberType? Type { get; set; }

        
    }
}