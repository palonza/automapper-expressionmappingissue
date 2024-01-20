namespace ExpressionMappingIssue
{
    internal class SourceChildType
    {
        public int Id { get; set; }
        public IEnumerable<SourceChildListItemType> ItemList { get; set; } // Uses same type (SourceListItemType) for its itemlist as SourceType
    }
    internal class SourceChildListItemType
    {
        public virtual int Id { get; set; }
    }
}
