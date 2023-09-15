using System;
using AutoMapper;
using CustomerDataAPI.Models;
using CustomerDataAPI.Models.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserUpdateRequestDTO, User>();
        CreateMap<AddressDTO, Address>();
    }
}

