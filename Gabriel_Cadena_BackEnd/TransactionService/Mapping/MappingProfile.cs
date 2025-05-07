using AutoMapper;
using TransactionService.DTOs;
using TransactionService.Models;

namespace TransactionService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(dest => dest.TransactionType,
                         opt => opt.MapFrom(src => src.TransactionType.ToString()))
                .ForMember(dest => dest.ProductName,
                         opt => opt.Ignore()); 
            CreateMap<CreateTransactionDTO, Transaction>()
                .ForMember(dest => dest.Id,
                         opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.TransactionDate,
                         opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.TransactionType,
                         opt => opt.MapFrom(src => Enum.Parse<TransactionType>(src.TransactionType)))
                .ForMember(dest => dest.TotalPrice,
                         opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));
        }
    }
}