using PersonalFinanceManagement.Domain.Commons;

namespace PersonalFinanceManagement.Domain.Entities;

public class Income : Auditable
{

    public int UserId { get; set; }
    public string Source { get; set; }
    public double Amount { get; set; }

}
