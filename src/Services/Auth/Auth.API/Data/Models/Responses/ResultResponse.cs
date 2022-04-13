namespace Auth.API.Data.Models.Responses
{
    public class ResultResponse<T>
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }

        public static ResultResponse<T> CreateSuccess(T value)
        {
            return new ResultResponse<T> { Successful = true, Value = value };
        }

        public static ResultResponse<T> CreateError(string message)
        {
            return new ResultResponse<T> { Successful = false, Message = message, Value = default };
        }
    }
}
