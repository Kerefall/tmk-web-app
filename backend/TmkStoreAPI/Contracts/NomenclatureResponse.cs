namespace TmkStoreAPI.Contracts
{
    public record NomenclatureResponse(
        Guid ID,
        Guid IDCat,
        Guid IDType,
        string IDTypeNew,
        string ProductionType,
        Guid? IDFunctionType,
        string Name,
        string Gost,
        string FormOfLength,
        string Manufacturer,
        string SteelGrade,
        decimal Diameter,
        decimal ProfileSize2,
        decimal PipeWallThickness,
        string Status,
        decimal Koef);
}