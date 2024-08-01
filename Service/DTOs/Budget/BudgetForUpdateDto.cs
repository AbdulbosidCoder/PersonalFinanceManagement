namespace PersonalFinanceManagement.Service.DTOs.Budget;

public class BudgetForUpdateDto
{

    public int UserId { get; set; }
    public string Category { get; set; }
    public double Amount { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }



}
