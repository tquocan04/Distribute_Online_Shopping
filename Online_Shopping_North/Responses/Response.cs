namespace Online_Shopping_North.Responses
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
