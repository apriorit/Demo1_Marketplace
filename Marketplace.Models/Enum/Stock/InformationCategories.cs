using System.Runtime.Serialization;

namespace Marketplace.Models.Dto.Enum.Stock
{
    public enum InformationCategories
    {
        [EnumMember(Value = "Main")]
        Main,
        [EnumMember(Value = "Additional")]
        Additional
    }
}
