using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Core.Models
{
    public class PhoneNumber
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneNumberId { get; set; }
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }

        public virtual Contact Contact { get; set; }
    }
}