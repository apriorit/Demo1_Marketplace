using System.Runtime.Serialization;

namespace Marketplace.Models.Enum.Payment
{
    public enum Language
    {
        [EnumMember(Value = "uk")]
        UK,
        [EnumMember(Value = "en")]
        EN,
        [EnumMember(Value = "ru")]
        RU,
    }
}
