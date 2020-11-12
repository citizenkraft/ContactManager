using ContactManager.Common.Dto;
using ContactManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Components
{
    public interface IContactComponent
    {
        Task<List<GetContactDto>> GetAllContactsAsync();
        Task<GetContactDto> GetContactAsync(string employeeId);
        Task<GetContactDto> GetContactAsync(int contactId);
        Task<List<GetContactDto>> GetContactsByLastNameAsync(string lastName);
        Task<List<GetContactDto>> GetRelatedContactsAsync(string employeeId);
        Task<List<GetContactDto>> GetRelatedContactsAsync(int contactId);
        Task<GetContactDto> CreateContactAsync(CreateOrUpdateContactDto dto);
        Task<GetContactDto> UpdateContactAsync(int contactId, CreateOrUpdateContactDto dto);
        Task<int> DeleteContactAsync(int contactId);

    }
}
