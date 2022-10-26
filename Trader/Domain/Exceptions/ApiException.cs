using System.Net;

namespace Trader.Api.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public string Erro { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public ApiException(string erro, HttpStatusCode statusCode) : base(erro)
        {
            Erro = erro;
            StatusCode = statusCode;
        }

        public ApiException(string erro, HttpStatusCode statusCode, string mensagem) : base(mensagem)
        {
            Erro = erro;
            StatusCode = statusCode;
        }
    }
}
