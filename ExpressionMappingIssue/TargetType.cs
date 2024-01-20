namespace ExpressionMappingIssue
{
    internal class TargetType
    {
        public virtual int Id { get; set; }

        public virtual TargetChildType Child { get; set; }

        public virtual ICollection<TargetListItemType> ItemList { get; set; } = new List<TargetListItemType>();
    }

    internal class TargetListItemType
    {
        public virtual int Id { get; set; }
    }
}
