namespace MoneyMeAPI.Errors
{
    public class APIException
    {
        public APIException(int statusCode, string message, string details = null)
        {
            Status = statusCode;
            Errors = message;
            Details = details;
        }

        public int Status { get; set; }
        public string Errors { get; set; }
        public string Details { get; set; }
    }
}
