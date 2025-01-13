namespace ScheduleLesson
{
    public class StatusCodeException : Exception
    {
        public int StatusCode { get; }
        public StatusCodeException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
