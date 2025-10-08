namespace TmkStore.Core.Models
{
    public class Nomenclature
    {
        public const int MAX_NAME_LENGTH = 500;
        public const int MAX_GOST_LENGTH = 250;
        public const int MAX_FORM_LENGTH = 100;
        public const int MAX_MANUFACTURER_LENGTH = 250;
        public const int MAX_STEEL_GRADE_LENGTH = 50;
        public const int MAX_STATUS_LENGTH = 100;

        private Nomenclature(
            Guid id,
            Guid idCat,
            Guid idType,
            string idTypeNew,
            string productionType,
            Guid? idFunctionType,
            string name,
            string gost,
            string formOfLength,
            string manufacturer,
            string steelGrade,
            decimal diameter,
            decimal profileSize2,
            decimal pipeWallThickness,
            string status,
            decimal koef)
        {
            ID = id;
            IDCat = idCat;
            IDType = idType;
            IDTypeNew = idTypeNew;
            ProductionType = productionType;
            IDFunctionType = idFunctionType;
            Name = name;
            Gost = gost;
            FormOfLength = formOfLength;
            Manufacturer = manufacturer;
            SteelGrade = steelGrade;
            Diameter = diameter;
            ProfileSize2 = profileSize2;
            PipeWallThickness = pipeWallThickness;
            Status = status;
            Koef = koef;
        }

        public Guid ID { get; }
        public Guid IDCat { get; }
        public Guid IDType { get; }
        public string IDTypeNew { get; } = string.Empty;
        public string ProductionType { get; } = string.Empty;
        public Guid? IDFunctionType { get; }
        public string Name { get; } = string.Empty;
        public string Gost { get; } = string.Empty;
        public string FormOfLength { get; } = string.Empty;
        public string Manufacturer { get; } = string.Empty;
        public string SteelGrade { get; } = string.Empty;
        public decimal Diameter { get; }
        public decimal ProfileSize2 { get; }
        public decimal PipeWallThickness { get; }
        public string Status { get; } = string.Empty;
        public decimal Koef { get; }

        public static (Nomenclature Nomenclature, string Error) Create(
            Guid id,
            Guid idCat,
            Guid idType,
            string idTypeNew,
            string productionType,
            Guid? idFunctionType,
            string name,
            string gost,
            string formOfLength,
            string manufacturer,
            string steelGrade,
            decimal diameter,
            decimal profileSize2,
            decimal pipeWallThickness,
            string status,
            decimal koef)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                error = "Наименование не может быть пустым или длиннее 500 символов";
            }
            else if (gost?.Length > MAX_GOST_LENGTH)
            {
                error = "ГОСТ/ТУ не может быть длиннее 250 символов";
            }
            else if (formOfLength?.Length > MAX_FORM_LENGTH)
            {
                error = "Форма поставки не может быть длиннее 100 символов";
            }
            else if (manufacturer?.Length > MAX_MANUFACTURER_LENGTH)
            {
                error = "Производитель не может быть длиннее 250 символов";
            }
            else if (steelGrade?.Length > MAX_STEEL_GRADE_LENGTH)
            {
                error = "Марка стали не может быть длиннее 50 символов";
            }
            else if (status?.Length > MAX_STATUS_LENGTH)
            {
                error = "Статус не может быть длиннее 100 символов";
            }

            var nomenclature = new Nomenclature(
                id,
                idCat,
                idType,
                idTypeNew ?? string.Empty,
                productionType ?? string.Empty,
                idFunctionType,
                name,
                gost ?? string.Empty,
                formOfLength ?? string.Empty,
                manufacturer ?? string.Empty,
                steelGrade ?? string.Empty,
                diameter,
                profileSize2,
                pipeWallThickness,
                status ?? string.Empty,
                koef);

            return (nomenclature, error);
        }
    }
}