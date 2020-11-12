using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Common.Dto;
using ContactManager.Core.Components;
using ContactManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private readonly IContactComponent _contactComponent;

        public ContactController(ILogger<ContactController> logger,
            IContactComponent contactComponent)
        {
            _logger = logger;
            _contactComponent = contactComponent;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetContactDto>>> GetContacts()
        {
            var results = await _contactComponent.GetAllContactsAsync();
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetContactDto>> GetContact(int id)
        {
            var result = await _contactComponent.GetContactAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("find-by-employee-id/{employeeId}")]
        public async Task<ActionResult<GetContactDto>> GetContactByEmployeeId(string employeeId)
        {
            var result = await _contactComponent.GetContactAsync(employeeId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("find-by-last-name/{lastName}")]
        public async Task<ActionResult<GetContactDto>> FindContactsByLastName(string lastName)
        {
            var result = await _contactComponent.GetContactsByLastNameAsync(lastName);
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("find-relations/{id}")]
        public async Task<ActionResult<List<GetContactDto>>> GetRelatedContacts(int id)
        {
            var result = await _contactComponent.GetRelatedContactsAsync(id);
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody] CreateOrUpdateContactDto contactDto)
        {
            var result = await _contactComponent.CreateContactAsync(contactDto);
            if (result.ContactId != 0)
            {
                return CreatedAtAction("GetContact", new { id = result.ContactId }, null);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("{contactId}")]
        public async Task<ActionResult> UpdateContact(int contactId, [FromBody] CreateOrUpdateContactDto contactDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var result = await _contactComponent.UpdateContactAsync(contactId, contactDto);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _contactComponent.DeleteContactAsync(id);
            if (result != 0)
            {
                return Ok();
            } else
            {
                return NotFound();
            }
        }

    }
}
