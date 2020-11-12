using ContactManager.Common.Dto.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ContactManager.Common.Dto
{
    public class CreateOrUpdateContactDto : IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public ContactType ContactType { get; set; }

        public virtual int? ParentId { get; set; }
        public virtual string EmployeeId { get; set; }
        public virtual DateTime? DateOfHire { get; set; }
        public virtual bool? CurrentlyEmployed { get; set; }


        public List<AddressDto> Addresses { get; set; }

        public List<PhoneNumberDto> PhoneNumbers { get; set; }



        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var vResults = new List<ValidationResult>();

            if (!PhoneNumbers.Any())
            {
                vResults.Add(new ValidationResult("At least one Phone number is required."));
            }
            else
            {
                if (!(PhoneNumbers.Select(x => x.Label).Distinct().Count() == PhoneNumbers.Count()))
                    vResults.Add(new ValidationResult("All phone number labels must be unique."));
            }

            if (!(Addresses.Select(x => x.Label).Distinct().Count() == Addresses.Count()))
                vResults.Add(new ValidationResult("All address labels must be unique."));

            switch (ContactType)
            {
                case ContactType.Employee:
                    if (string.IsNullOrEmpty(EmployeeId))
                        vResults.Add(new ValidationResult("EmployeeId required"));
                    if (!DateOfHire.HasValue)
                        vResults.Add(new ValidationResult("Date of Hire required"));
                    if (!CurrentlyEmployed.HasValue)
                        vResults.Add(new ValidationResult("Currently Employed required"));
                    break;
                case ContactType.Spouse:
                case ContactType.Dependant:
                default:
                    if (!ParentId.HasValue)
                        vResults.Add(new ValidationResult("Parent Id required for Employee Spouse/Relation"));
                    break;
            }

            return vResults;
        }
    }
}
