namespace TechPortal.Models.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public Result() { }

        public Result(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}

