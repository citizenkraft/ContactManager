using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Core.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public string Label { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public virtual Contact Contact { get; set; }

    }
}