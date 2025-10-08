namespace TmkStoreAPI.Contracts
{
    public record PipeTypeRequest(
        string Type,
        Guid IDParentType);
}