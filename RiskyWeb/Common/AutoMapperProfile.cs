using AutoMapper;
using Lokad.Cloud.Storage;
using RiskyWeb.Models.Analysis;
using RiskyWeb.Models.Action;

namespace RiskyWeb.Common
{
    public class AutoMapperProfile : Profile
    {
        public const string VIEW_MODEL = "RiskyWeb";

        public override string ProfileName
        {
            get
            {
                return VIEW_MODEL;
            }
        }

        protected override void Configure()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            // analyses
            CreateMap<CloudEntity<Analysis>, AnalysisView>()
                .ForMember(target => target.AnalysisRowKey, opt => opt.MapFrom(m => m.RowKey))
                .ForMember(target => target.AnalysisPartKey, opt => opt.MapFrom(m => m.PartitionKey))
                .ForMember(target => target.Created, opt => opt.MapFrom(m => m.Value.Created))
                .ForMember(target => target.Title, opt => opt.MapFrom(m => m.Value.Title))
                .ForMember(target => target.Description, opt => opt.MapFrom(m => m.Value.Description));

            CreateMap<AnalysisCreate, Analysis>();
            CreateMap<AnalysisView, Analysis>();

            // actions
            CreateMap<AnalysisActionView, AnalysisAction>();

            CreateMap<CloudEntity<AnalysisAction>, AnalysisActionView>()
                .ForMember(target => target.AnalysisActionRowKey, opt => opt.MapFrom(m => m.RowKey))
                .ForMember(target => target.AnalysisActionPartKey, opt => opt.MapFrom(m => m.PartitionKey))
                .ForMember(target => target.Created, opt => opt.MapFrom(m => m.Value.Created))
                .ForMember(target => target.Title, opt => opt.MapFrom(m => m.Value.Title))
                .ForMember(target => target.Deadline, opt => opt.MapFrom(m => m.Value.Deadline))
                .ForMember(target => target.CategoryName, opt => opt.MapFrom(m => m.Value.Category.Description))
                .ForMember(target => target.AnalysisIds, opt => opt.MapFrom(m => m.Value.AnalysisIds));
        }
    }
}