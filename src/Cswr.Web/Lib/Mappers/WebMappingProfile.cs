namespace Cswr.Bal.Lib.Mappers
{
    using AutoMapper;
    using Cswr.Bal.Domain;
    using Cswr.Web.Models;

    /// <summary>
    /// A class for declaring how we'll map entity objects from EF into domain objects.
    /// </summary>
    public class WebMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebMappingProfile"/> class.
        /// </summary>
        public WebMappingProfile()
        {
            // Model -> BLL
            this.CreateMap<EditSheetViewModel, CheatSheet>();
            //this.CreateMap<CheatSheetViewModel, CheatSheet>();

            // BLL -> Model
            this.CreateMap<Player, EditSheetViewModel.PlayerDetails>();
            this.CreateMap<CheatSheetItem, EditSheetViewModel.CheatSheetItemDetails>();
        }
    }
}
