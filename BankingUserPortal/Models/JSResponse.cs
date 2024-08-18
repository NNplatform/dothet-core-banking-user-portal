namespace BankingUserPortal.Models
{
    public class JSResponse<T>
    {
        public string Code { get; set; } = "0"; // Default to "0" for success
        public string Message { get; set; }
        public string Cause { get; set; }
        public T Result { get; set; }

        public JSResponse() { }

        public JSResponse(string code, string message = null, string cause = null, T result = default)
        {
            Code = code;
            Message = message;
            Cause = cause;
            Result = result;
        }

        public static JSResponse<T> Success(T result, string message = null)
        {
            return new JSResponse<T>("0", message, result: result);
        }

        public static JSResponse<T> Failure(string message, string cause = null)
        {
            return new JSResponse<T>("-1", message, cause);
        }
    }
}
