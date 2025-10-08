namespace TmkStore.DataAccess.Entities
{
    public class PipeTypeEntity
    {
        public Guid IDType { get; set; }
        public string Type { get; set; } = string.Empty;
        public Guid IDParentType { get; set; }
    }
}