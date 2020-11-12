using AutoMapper;
using ContactManager.Common.Dto;
using ContactManager.Common.Dto.Enums;
using ContactManager.Core.Models;
using ContactManager.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Components
{
    public class ContactComponent : IContactComponent
    {
        private ILogger<ContactComponent> _logger;
        private IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactComponent(ILogger<ContactComponent> logger,
            IContactRepository contactRepository,
            IMapper mapper)
        {
            _logger = logger;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<GetContactDto> CreateContactAsync(CreateOrUpdateContactDto dto)
        {
            var contact = _mapper.Map<Contact>(dto);

            await ValidateOnlyOneSpouseAsync(contact);

            var result = await _contactRepository.CreateAsync(contact);
            return _mapper.Map<GetContactDto>(result);
        }

        public async Task<int> DeleteContactAsync(int contactId)
        {
            var contact = await _contactRepository.GetHydratedContactAsync(contactId);
            if (contact != null)
            {
                return await _contactRepository.DeleteAsync(contact);
            }
            else
            {
                return 0;
            }
        }

        public async Task<GetContactDto> GetContactAsync(string employeeId)
        {
            var contact = await _contactRepository.GetByEmployeeIdAsync(employeeId);
            return _mapper.Map<GetContactDto>(contact);
        }

        public async Task<GetContactDto> GetContactAsync(int contactId)
        {
            var contact = await _contactRepository.GetHydratedContactAsync(contactId);
            return _mapper.Map<GetContactDto>(contact);
        }

        public async Task<List<GetContactDto>> GetAllContactsAsync()
        {
            var results = await _contactRepository.GetAllAsync();
            return _mapper.Map<List<GetContactDto>>(results);
        }

        public async Task<List<GetContactDto>> GetContactsByLastNameAsync(string lastName)
        {
            var results = await _contactRepository.FindByLastNameAsync(lastName);
            return _mapper.Map<List<GetContactDto>>(results);
        }

        public async Task<List<GetContactDto>> GetRelatedContactsAsync(string employeeId)
        {
            var results = await _contactRepository.GetRelatedContactsAsync(employeeId);
            return _mapper.Map<List<GetContactDto>>(results);
        }

        public async Task<List<GetContactDto>> GetRelatedContactsAsync(int contactId)
        {
            var results = await _contactRepository.GetRelatedContactsAsync(contactId);
            return _mapper.Map<List<GetContactDto>>(results);
        }

        public async Task<GetContactDto> UpdateContactAsync(int contactId, CreateOrUpdateContactDto dto)
        {
            var contact = _mapper.Map<Contact>(dto);
            await ValidateOnlyOneSpouseAsync(contact);
            var result = await _contactRepository.UpdateContactAsync(contactId, contact);
            return _mapper.Map<GetContactDto>(result);
        }

        private async Task ValidateOnlyOneSpouseAsync(Contact contact)
        {
            if (contact.ContactType == ContactType.Spouse)
            {
                var employeeRelations = await GetRelatedContactsAsync(contact.ParentId.Value);
                if (employeeRelations.Any(x => x.ContactType == ContactType.Spouse && x.ContactId != contact.ContactId))
                {
                    throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Employee may only have one spouse");
                }
            }
        }

    }
}
