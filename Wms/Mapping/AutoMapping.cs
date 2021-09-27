using AutoMapper;
using Wms.API.Models;
using Wms.ViewModel.Dialog;

namespace Wms.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            MappedCustomer();
            MappedBranchVm();
            MappedBranchBaseVm();
        }

        private void MappedBranchVm()
        {
            CreateMap<DisplayAlertBranchViewModel, BranchBase>()
                .ForMember(f => f.Code, map => map.MapFrom(opt => opt.Country.CountryCode));
            CreateMap<DisplayAlertBranchViewModel, BranchCreate>()
                .ForMember(f => f.Code, map => map.MapFrom(opt => opt.Country.CountryCode));
        }


        private  void MappedBranchBaseVm()
        {
            CreateMap<DisplayAlertBranchBaseViewModel, Account>();
        }


        private void MappedCustomer()
        {
            CreateMap<Account, Customer>().ForMember(f => f.Zip, m => m.MapFrom(opt => opt.Zip))
                .ForMember(f => f.Name, m => m.MapFrom(opt => opt.Name))
                .ForMember(f => f.City, m => m.MapFrom(opt => opt.City))
                .ForMember(f => f.Phone, m => m.MapFrom(opt => opt.Phone))
                .ForMember(f => f.State, m => m.MapFrom(opt => opt.State))
                .ForMember(f => f.Email, m => m.MapFrom(opt => opt.Email))
                .ForMember(f => f.Company, m => m.MapFrom(opt => opt.Company))
                .ForMember(f => f.Address, m => m.MapFrom(opt => opt.Address))
                .ForMember(f => f.CountryName, m => m.Ignore())
                .ForMember(f => f.Id, m => m.Ignore())
                .ForMember(f => f.Language, m => m.Ignore())
                .ForMember(f => f.Logo, m => m.Ignore())
                .ForMember(f => f.Role, m => m.Ignore());
        }
    }
}
