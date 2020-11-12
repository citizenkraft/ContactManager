using AutoMapper;
using ContactManager.Common.Dto;
using ContactManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Api.Profiles
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{	
			CreateMap<Contact, GetContactDto>();
			CreateMap<CreateOrUpdateContactDto, Contact>();
			CreateMap<PhoneNumber, PhoneNumberDto>();
			CreateMap<PhoneNumberDto, PhoneNumber>();
			CreateMap<Address, AddressDto>();
			CreateMap<AddressDto, Address>();
		}
	}
}

