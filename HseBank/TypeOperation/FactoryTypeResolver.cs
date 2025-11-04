namespace HseBank.TypeOperation;

public class FactoryTypeResolver : IFactoryTypeResolver
{
    private Dictionary<string, ITypeOperationFactory> factories =  new Dictionary<string, ITypeOperationFactory>
    {
        {"доход", new ProfitFactory()},
        {"расход", new ExpenseFactory()},
    };
    
    public ITypeOperationFactory GetFactory(string typeName)
    {
        typeName = typeName.ToLower();
        if (factories.ContainsKey(typeName))
        {
            return factories[typeName];
        }

        throw new ArgumentException("Неправильное название типа операции");
    }
}