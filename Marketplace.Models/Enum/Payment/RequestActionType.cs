using System.Runtime.Serialization;

namespace Marketplace.Models.Enum.Payment
{
    public enum RequestActionType
    {
        [EnumMember(Value = "pay")]
        Pay = 1,
        [EnumMember(Value = "hold")]
        Hold = 2,
        [EnumMember(Value = "subscribe")]
        Subscribe = 3,
        [EnumMember(Value = "paydonate")]
        Paydonate = 4,
        [EnumMember(Value = "auth")]
        Auth = 5,
    }
}
