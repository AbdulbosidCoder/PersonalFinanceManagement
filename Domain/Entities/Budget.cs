using PersonalFinanceManagement.Domain.Commons;
using System.ComponentModel;

namespace PersonalFinanceManagement.Domain.Entities;

public class Budget : Auditable
{
    public int UserId { get; set; }
    public string Category { get; set;}
    public double Amount { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set;}
        

}
