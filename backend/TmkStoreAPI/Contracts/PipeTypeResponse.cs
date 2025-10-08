namespace TmkStoreAPI.Contracts
{
    public record PipeTypeResponse(
        Guid IDType,
        string Type,
        Guid IDParentType);
}