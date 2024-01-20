namespace ExpressionMappingIssue
{
    internal class SourceType
    {
        public int Id { get; set; }
        public SourceChildType Child { set; get;}
        public IEnumerable<SourceListItemType> ItemList { get; set; }
    }

    internal class SourceListItemType
    {
        public int Id { get; set; }
    }
}
