namespace BuberBreakfast.Domain.Entities;

public class User 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    public object Value1 { get; }
    public object Value2 { get; }
    public object Value3 { get; }
    public object Value4 { get; }
}