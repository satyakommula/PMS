using AutoMapper;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Configuration
{
    public class AutoMapperProfile: Profile
    {
       
        public AutoMapperProfile()
        {
            CreateMap<SignUpModel, tblUser>(MemberList.None)
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.TempEmail, opt => opt.Ignore())
                .ForMember(dest => dest.RoleID, opt => opt.Ignore())
                .ForMember(dest => dest.EmpId, opt => opt.Ignore())
                .ForMember(dest => dest.Acive, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.IpAddress, opt => opt.Ignore())
                .ForMember(dest => dest.Mobile, opt => opt.Ignore())
                .ForMember(dest => dest.Verified, opt => opt.Ignore())
                .ForMember(dest => dest.VerifcationCode, opt => opt.Ignore())
                .ForMember(dest => dest.VerifiedDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.Other, opt => opt.Ignore());

        }
    }
}