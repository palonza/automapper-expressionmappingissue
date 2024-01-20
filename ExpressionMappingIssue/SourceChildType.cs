namespace ExpressionMappingIssue
{
    internal class SourceChildType
    {
        public int Id { get; set; }
        public IEnumerable<SourceListItemType> ItemList { get; set; } // Uses same type (SourceListItemType) for its itemlist as SourceType
    }
}
