using AutoMapper;

namespace Lib.Api.Profiles {
    public class IPDetailsProfile : Profile {
        public IPDetailsProfile () {

            CreateMap<Lib.Service.IIPDetails, Lib.Core.Models.IPDetails> ();
            CreateMap<Lib.Data.Entities.IPDetails, Lib.Core.Models.IPDetails> ();
            CreateMap<Lib.Service.IIPDetails, Lib.Data.Entities.IPDetails> ();
        }

    }
}