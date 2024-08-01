namespace PersonalFinanceManagement.Service.Exceptions;

public class PersonalFinanceException : Exception
{
    public int StatusCode { get; set; }

    public PersonalFinanceException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
