using ContactManager.Common.Dto.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Common.Dto
{
    public class GetContactDto
    {
        public virtual int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ContactType ContactType { get; set; }

        public virtual int? ParentId { get; set; }
        public virtual string EmployeeId { get; set; }
        public virtual DateTime? DateOfHire { get; set; }
        public virtual bool? CurrentlyEmployed { get; set; }

        public List<AddressDto> Addresses { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }

    }
}
