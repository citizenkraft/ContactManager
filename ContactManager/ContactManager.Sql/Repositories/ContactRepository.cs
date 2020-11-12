using ContactManager.Common.Dto.Enums;
using ContactManager.Core.Models;
using ContactManager.Core.Repositories;
using ContactManager.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data.Sql.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ContactManagerDataContext context) : base(context)
        {
        }

        public async Task<Contact> GetHydratedContactAsync(int contactId)
        {
            return await _context.Contacts
                .Include(x => x.Addresses)
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync(x => x.ContactId == contactId);
        }

        public async Task<List<Contact>> FindByLastNameAsync(string lastName)
        {
            return await _context.Contacts
                .Include(x => x.Addresses)
                .Include(x => x.PhoneNumbers)
                .Where(x => x.LastName.ToLower().StartsWith(lastName.ToLower())).ToListAsync();
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts
                .Include(x => x.Addresses)
                .Include(x => x.PhoneNumbers)
                .Where(x=> x.ContactType == ContactType.Employee).ToListAsync();
        }

        public async Task<Contact> GetByEmployeeIdAsync(string employeeId)
        {
            return await _context.Contacts
                .Include(x => x.Addresses)
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<List<Contact>> GetRelatedContactsAsync(string employeeId)
        {
            return await _context.Contacts
                .Include(x => x.Addresses)
                .Include(x => x.PhoneNumbers)
                .Where(x => x.Parent.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<List<Contact>> GetRelatedContactsAsync(int contactId)
        {
            return await _context.Contacts
                .Include(x=> x.Addresses)
                .Include(x=> x.PhoneNumbers)
                .Where(x => x.ParentId == contactId).ToListAsync();
        }

        public async Task<Contact> UpdateContactAsync(int contactId, Contact contact)
        {
            var originalContact = await GetHydratedContactAsync(contactId);
            if (originalContact != null)
            {
                _context.Entry(originalContact).CurrentValues.SetValues(contact);

                foreach (var address in contact.Addresses)
                {
                    address.ContactId = contact.ContactId;
                    var updateAddress = originalContact.Addresses.FirstOrDefault(x => x.AddressId == address.AddressId);
                    if (updateAddress != null)
                    {
                        _context.Entry(updateAddress).CurrentValues.SetValues(address);
                    }
                    else
                    {
                        originalContact.Addresses.Add(address);
                    }
                }

                foreach (var phoneNumber in contact.PhoneNumbers)
                {
                    phoneNumber.ContactId = contact.ContactId;
                    var updatePhoneNumber = originalContact.PhoneNumbers.FirstOrDefault(x => x.PhoneNumberId == phoneNumber.PhoneNumberId);
                    if (updatePhoneNumber != null)
                    {
                        _context.Entry(updatePhoneNumber).CurrentValues.SetValues(phoneNumber);
                    }
                    else
                    {
                        originalContact.PhoneNumbers.Add(phoneNumber);
                    }
                }
                _context.SaveChanges();
            }
            return originalContact;
        }
    }
}
