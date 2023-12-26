using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CleanArchitecture.Model.Commons
{
    public class ReturnData<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public T? Data { get; set; }
        private ErrorMessageReceiver? MessageReceiverEnum { get; set; }
        public string? MessageReceiver => MessageReceiverEnum?.ToString() ?? null;

        public static ReturnData<T> Success()
        {
            return new ReturnData<T> { Data = default };
        }

        public static ReturnData<T> Success(T? data = default, dynamic? dynamicData = null, string message = "")
        {
            return new ReturnData<T> { Data = data, Message = message };
        }

        public static ReturnData<T> Fail(string? errorMessage = null, ErrorMessageReceiver messageReceiver = Commons.ErrorMessageReceiver.Developer)
        {
            return new ReturnData<T> { IsSuccess = false, Message = errorMessage, MessageReceiverEnum = messageReceiver};
        }

        public void SetMessageReceiver(ErrorMessageReceiver receiver)
        {
            MessageReceiverEnum = receiver;
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return System.Text.Json.JsonSerializer.Serialize(this, options);
        }
    }

    public enum ErrorMessageReceiver
    { 
        User = 1,
        Developer = 2
    }

    public enum ErrorCodeEnum
    {
        [Description("Başarılı")]
        Success = 0,
        [Description("İç Sunucu Hatası")]
        InternalServerError = 500,
        [Description("Bozuk İstek")]
        BadRequest = 400,
        [Description("Yetkisiz")]
        Unauthorized = 401,
        [Description("Yetkisiz İşlem")]
        Forbidden = 403,
        [Description("Sonuç Bulunamadı")]
        NotFound = 404,

        #region User Error Codes 1000
        [Description("Kullanıcı Adı ve Şifrenizi Kontrol Ederek Tekrar Giriniz")]
        UserNotFound = 1001,
        [Description("Kullanıcı Oluşturulamadı")]
        UserNotCreated = 1002,
        [Description("Hatalı Kod")]
        Aut2CodeWrong = 1003,
        [Description("Hatalı Oturum Anahtarı")]
        TokenIsNotTrue = 1004,
        [Description("Token Süresi Dolmuş")]
        TokenExpired = 1005,
        [Description("Şifremi Unuttum Maili Gönderilemedi")]
        ForgotMailNotSending = 1006,
        [Description("Kullanıcı Güncellenemedi")]
        UserNotUpdated = 1007,
        [Description("Rol Yetki Hatası")]
        RoleAuthorizationError = 1008,
        [Description("Mevcut Parola Hatalı")]
        OldPasswordIsWrong = 1009,
        [Description("Yeni Parola Gönderilemedi")]
        NewPasswordMailNotSending = 1010,
        [Description("Parolalar Eşleşmiyor")]
        PasswordsNotMatch = 1011,
        [Description("Kullanıcı Daha Önce Oluşturulmuş")]
        UserExists = 1012,
        [Description("Kullanıcı Doğrulama SMS Kodu Gönderilemedi")]
        CreateUserSmsSendError = 1013,
        #endregion

        #region MyRegion
        [Description("Ödeme İade İşlemine Devam Edilemiyor")]
        RefunPaymentProviderError = 1014,
        #endregion

        #region Parking Ticket
        [Description("Otopark Ücreti Ödeme Bildirimi Gönderilemedi")]
        ParkingTicketPaymentRegisterError = 1015,
        #endregion

    }
}
