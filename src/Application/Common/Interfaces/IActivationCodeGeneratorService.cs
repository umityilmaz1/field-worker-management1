namespace CleanArchitecture.Application.Common.Interfaces;
public interface IActivationCodeGeneratorService
{
    Task<string> GenerateActivationCode();
}
