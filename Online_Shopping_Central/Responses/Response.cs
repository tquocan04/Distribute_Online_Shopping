namespace Online_Shopping_Central.Responses
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
