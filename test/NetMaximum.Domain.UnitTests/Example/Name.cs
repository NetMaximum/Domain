namespace NetMaximum.Domain.UnitTests.Examples;

public class Name : Value<Name>
{
    public string FirstName { get; }
    public string Surname { get; }

    [Newtonsoft.Json.JsonConstructor]
    private Name(string firstName, string surname)
    {
        FirstName = firstName;
        Surname = surname;
    }
    
    public static Name FromString(string firstName, string surname)
    {
        return new Name(firstName, surname);
    }
}