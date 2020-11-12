using ContactManager.Common.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Core.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ContactType ContactType { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? DateOfHire { get; set; }
        public bool? CurrentlyEmployed { get; set; }
        public int? ParentId { get; set; }

        public Contact Parent { get; set; }
        public virtual List<Contact> Relations { get; set; }
        public virtual List<PhoneNumber> PhoneNumbers { get; set; }
        public virtual List<Address> Addresses { get; set; }

    }
}
