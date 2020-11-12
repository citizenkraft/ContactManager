using ContactManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<Contact> GetHydratedContactAsync(int contactId);
        Task<Contact> GetByEmployeeIdAsync(string employeeId);
        Task<List<Contact>> FindByLastNameAsync(string lastName);
        Task<List<Contact>> GetRelatedContactsAsync(string employeeId);
        Task<List<Contact>> GetRelatedContactsAsync(int contactId);
        Task<List<Contact>> GetAllAsync();
        Task<Contact> UpdateContactAsync(int contactId, Contact contact);
    }
}
