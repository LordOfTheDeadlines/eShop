namespace Identity.API.Data.Models.Response
{
    public class ResultModel<T>
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }

        public static ResultModel<T> CreateSuccess(T value)
        {
            return new ResultModel<T> { Successful = true, Value = value };
        }

        public static ResultModel<T> CreateError(string message)
        {
            return new ResultModel<T> { Successful = false, Message = message, Value = default };
        }
    }
}
