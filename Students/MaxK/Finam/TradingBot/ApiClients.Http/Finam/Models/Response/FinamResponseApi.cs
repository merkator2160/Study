namespace ApiClientsHttp.Finam.Models.Response
{
    public class FinamResponseApi<T>
    {
        public T Data { get; set; }
        public FinamErrorResponseApi Error { get; set; }
    }
}