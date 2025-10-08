namespace TmkStore.DataAccess.Entities
{
    public class NomenclatureEntity
    {
        public Guid ID { get; set; }
        public Guid IDCat { get; set; }
        public Guid IDType { get; set; }
        public string IDTypeNew { get; set; } = string.Empty;
        public string ProductionType { get; set; } = string.Empty;
        public Guid? IDFunctionType { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gost { get; set; } = string.Empty;
        public string FormOfLength { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string SteelGrade { get; set; } = string.Empty;
        public decimal Diameter { get; set; }
        public decimal ProfileSize2 { get; set; }
        public decimal PipeWallThickness { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Koef { get; set; }
    }
}