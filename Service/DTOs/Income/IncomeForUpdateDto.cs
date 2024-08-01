namespace PersonalFinanceManagement.Service.DTOs.Income;

public class IncomeForUpdateDto
{
    public int UserId { get; set; }
    public string Source { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; }


}
