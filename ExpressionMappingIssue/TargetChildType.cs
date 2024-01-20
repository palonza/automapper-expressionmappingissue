namespace ExpressionMappingIssue
{
    internal class TargetChildType
    {
        public virtual int Id { get; set; }
        public virtual ICollection<TargetChildListItemType> ItemList { get; set; } = new List<TargetChildListItemType>();
    }

    internal class TargetChildListItemType
    {
        public virtual int Id { get; set; }
    }
}
