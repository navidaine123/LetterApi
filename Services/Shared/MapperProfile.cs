using AutoMapper;
using Models.MessageModels;
using Services.Dto;
using Services.Dto.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Models.UserModels;

namespace Services.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap()
                .ForMember(x => x.Id, option => option.Ignore());

            CreateMap<User, RegisterDto>()
                .ReverseMap()
                .ForMember(x => x.Id, option => option.Ignore());

            CreateMap<Message, SendMsgDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, option => option.Ignore())
                .ForMember(x=>x.CreatedBy,option=>option.Ignore());

            CreateMap<MessageSender, SendMsgDTO>()
                .ForMember
                (x => x.To, option => option
                   .MapFrom(src => src.MessageRecievers
                   .Where(p => p.IsCc == false).Select(x => x.UserId)))
                   .ForMember
                (x => x.Cc, option => option
                   .MapFrom(src => src.MessageRecievers
                   .Where(p => p.IsCc == true).Select(x => x.UserId)));

            CreateMap<MessageReciever, SendMsgDTO>();
        }
    }
}