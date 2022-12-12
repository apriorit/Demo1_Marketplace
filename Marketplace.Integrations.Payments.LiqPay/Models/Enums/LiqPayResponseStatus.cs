using System.Runtime.Serialization;

namespace Marketplace.Integrations.Payments.LiqPay.Models.Enums
{
    public enum LiqPayResponseStatus
    {
        [EnumMember(Value = "error")]
        Error = 1,
        [EnumMember(Value = "failure")]
        Failure,
        [EnumMember(Value = "reversed")]
        Reversed,
        [EnumMember(Value = "subscribed")]
        Subscribed,
        [EnumMember(Value = "success")]
        Success,
        [EnumMember(Value = "unsubscribed")]
        Unsubscribed,

        [EnumMember(Value = "3ds_verify")]
        _3DS_verify,
        [EnumMember(Value = "captcha_verify")]
        CaptchaVerify,
        [EnumMember(Value = "cvv_verify")]
        CvvVerify,
        [EnumMember(Value = "ivr_verify")]
        IvrVerify,
        [EnumMember(Value = "otp_verify")]
        OtpVerify,
        [EnumMember(Value = "password_verify")]
        PasswordVerify,
        [EnumMember(Value = "phone_verify")]
        PhoneVerify,
        [EnumMember(Value = "pin_verify")]
        PinVerify,
        [EnumMember(Value = "receiver_verify")]
        ReceiverVerify,
        [EnumMember(Value = "sender_verify")]
        SenderVerify,
        [EnumMember(Value = "senderapp_verify")]
        SenderappVerify,
        [EnumMember(Value = "wait_qr")]
        WaitQr,
        [EnumMember(Value = "wait_sender")]
        WaitSender,

        [EnumMember(Value = "cash_wait")]
        CashWait,
        [EnumMember(Value = "hold_wait")]
        HoldWait,
        [EnumMember(Value = "invoice_wait")]
        InvoiceWait,
        [EnumMember(Value = "prepared")]
        Prepared,
        [EnumMember(Value = "processing")]
        Processing,
        [EnumMember(Value = "wait_accept")]
        WaitAccept,
        [EnumMember(Value = "wait_card")]
        WaitCard,
        [EnumMember(Value = "wait_compensation")]
        WaitCompensation,
        [EnumMember(Value = "wait_lc")]
        WaitLc,
        [EnumMember(Value = "wait_reserve")]
        WaitReserve,
        [EnumMember(Value = "wait_secure")]
        WaitSecure,
    }
}
