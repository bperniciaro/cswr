using AutoMapper;
using Cswr.Bal.Domain;
using Cswr.Dal.Models;

namespace Cswr.Bal.Lib.Mappers
{
    /// <summary>
    /// A class for declaring how we'll map entity objects from EF into domain objects.
    /// </summary>
    public class BalMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BalMappingProfile"/> class.
        /// </summary>
        public BalMappingProfile()
        {
            // BAL -> DAL
            this.CreateMap<SheetsCheatSheet, CheatSheet>().ForMember(x => x.CheatSheetItems, o => o.MapFrom(s => s.SheetsCheatSheetItems));
            this.CreateMap<SheetsCheatSheetItem, CheatSheetItem>();
            this.CreateMap<SheetsPlayer, Player>();

            // DAL -> BAL
            this.CreateMap<CheatSheet, SheetsCheatSheet>().ForMember(x => x.SheetsCheatSheetItems, o => o.MapFrom(s => s.CheatSheetItems));
        }
    }
}
