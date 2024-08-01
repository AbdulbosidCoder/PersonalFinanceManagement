namespace PersonalFinanceManagement.Service.DTOs.User;

public class UserForCreationDto
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }


}
