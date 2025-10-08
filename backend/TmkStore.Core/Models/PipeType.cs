namespace TmkStore.Core.Models
{
    public class PipeType
    {
        public const int MAX_TYPE_LENGTH = 250;

        private PipeType(Guid idType, string type, Guid idParentType)
        {
            IDType = idType;
            Type = type;
            IDParentType = idParentType;
        }

        public Guid IDType { get; }
        public string Type { get; } = string.Empty;
        public Guid IDParentType { get; }

        public static (PipeType PipeType, string Error) Create(Guid idType, string type, Guid idParentType)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(type) || type.Length > MAX_TYPE_LENGTH)
            {
                error = "Название типа не может быть пустым или длиннее 250 символов";
            }

            var pipeType = new PipeType(idType, type, idParentType);
            return (pipeType, error);
        }
    }
}