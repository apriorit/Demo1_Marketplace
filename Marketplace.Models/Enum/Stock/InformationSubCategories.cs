using System.Runtime.Serialization;

namespace Marketplace.Models.Dto.Enum.Stock
{
    public enum InformationSubCategories
    {
        [EnumMember(Value = "Size")]
        Size,
        [EnumMember(Value = "Type")]
        Type,
        [EnumMember(Value = "PathToImage")]
        PathToImage
    }
}
