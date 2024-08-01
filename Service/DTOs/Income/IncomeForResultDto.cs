using PersonalFinanceManagement.Service.DTOs.User;

namespace PersonalFinanceManagement.Service.DTOs.Income;

public class IncomeForResultDto
{
    public int Id { get; set; }
    public UserForResultDto User { get; set; }
    public string Source { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; }

}
