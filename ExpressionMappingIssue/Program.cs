using AutoMapper.Extensions.ExpressionMapping;
using System.Linq.Expressions;

namespace ExpressionMappingIssue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mapper = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping(); // https://docs.automapper.org/en/stable/Expression-Translation-(UseAsDataSource).html

                cfg.CreateMap<SourceType, TargetType>().ReverseMap();
                cfg.CreateMap<SourceChildType, TargetChildType>().ReverseMap();

                // Same source type can map to different target types. This seems unsupported currently.
                cfg.CreateMap<SourceListItemType, TargetListItemType>().ReverseMap();
                cfg.CreateMap<SourceChildListItemType, TargetChildListItemType>().ReverseMap();

            }).CreateMapper();

            IEnumerable<SourceType> sources = [
                new()
                {
                    Id = 1,
                    Child = new() { ItemList = [new()] },
                    ItemList = [new(), new()]
                },
                new()
                {
                    Id = 2,
                    Child = new() { ItemList = [] },
                    ItemList = []
                },
                new()
                {
                    Id = 3,
                    Child = new() { ItemList = null },
                    ItemList = null
                },
            ];

            // Simple mapping, no problem here
            var targets = mapper.Map<IEnumerable<TargetType>>(sources)
                .ToList();

            // The expression targeting SourceType, that we want to translate to an expression on TargetType1
            Expression<Func<SourceType, bool>> sourcesWithListItemsExpr = src => src.Id != 0 && src.ItemList != null && src.ItemList.Any(); // Sources with non-empty ItemList

            // Try the source expression on sources itself, works fine
            var sourcesWithListItems = sources
                .AsQueryable().Where(sourcesWithListItemsExpr)
                .ToList();

            //Expression<Func<TargetType1, bool>> target1sWithListItemsExpr = mapper.Map<Expression<Func<TargetType1, bool>>>(sourcesWithListItemsExpr);
            Expression<Func<TargetType, bool>> target1sWithListItemsExpr = mapper.MapExpression<Expression<Func<TargetType, bool>>>(sourcesWithListItemsExpr);

            // Get the TargetTypes that match the translated expression
            var target1sWithListItems = targets
                .AsQueryable().Where(target1sWithListItemsExpr) // Without AddExpressionMapping() this line throws an UnreachableException
                .ToList();
            Console.WriteLine($"source list: {sourcesWithListItems.Count}");
            Console.WriteLine($"target list: {target1sWithListItems.Count}");
        }
    }
}
