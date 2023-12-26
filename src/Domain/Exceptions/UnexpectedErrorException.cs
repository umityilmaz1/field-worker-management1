namespace CleanArchitecture.Domain.Exceptions;

public class UnexpectedErrorException : Exception
{
    public UnexpectedErrorException()
        : base($"Beklenmeyen hata oluştu.") //UMIT: mesaj dil dosyası ile yönetilmeli
    {
    }
}
