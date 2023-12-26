using System.Net;
using CleanArchitecture.Domain.Common;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.Common.Exceptions;
public class PaycellException : BaseException
{
    public PaycellException(string message, string code, ErrorMessageReceiver receiver) : base(GetMessage(message, code), HttpStatusCode.InternalServerError, receiver)
    {
    }

    public PaycellException(string threeDResultCode, string threeDResultMessage, string mdStatusCode, string mdStatusMessage, ErrorMessageReceiver receiver) : base(GetThreeDMessage(threeDResultCode, threeDResultMessage, mdStatusCode, mdStatusMessage), HttpStatusCode.InternalServerError, receiver)
    {
    }

    private static string GetMessage(string message, string code)
    { 
        return $"Paycell Exception --> Error Code : {code} Error Description : {message}";
    }

    private static string GetThreeDMessage(string threeDResultCode, string threeDResultMessage, string mdStatusCode, string mdStatusMessage)
    {
        return $"Paycell Callback Exceptiopn \n Result : {threeDResultCode} \n Result Description : {threeDResultMessage} \n MdStatus : {mdStatusCode} \n MdErrorMessage : {mdStatusMessage}";
    }
}
