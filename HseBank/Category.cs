namespace HseBank;

public class Category
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
    
    public Category(int id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }
}