using System.Net;

namespace Trader.Api.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public string Erro { get; private set; }
        public TraderApiError StatusCode { get; private set; }

        public ApiException(string erro, TraderApiError statusCode) : base(erro)
        {
            Erro = erro;
            StatusCode = statusCode;
        }

        public ApiException(string erro, TraderApiError statusCode, string mensagem) : base(mensagem)
        {
            Erro = erro;
            StatusCode = statusCode;
        }
    }
}
