namespace HseBank.TypeOperation;

public class FactoryTypeResolver : IFactoryTypeResolver
{
    private Dictionary<string, ITypeOperationFactory> factories =  new Dictionary<string, ITypeOperationFactory>
    {
        {"доход", new ProfitFactory()},
        {"расход", new ExpenseFactory()},
    };

    private Dictionary<string, string> Conversely = new Dictionary<string, string>()
    {
        { "доход", "расход" },
        { "расход", "доход" },
    };
    
    public ITypeOperationFactory GetFactory(string typeName, bool flagIsConversely)
    {
        typeName = typeName.ToLower();
        if (factories.ContainsKey(typeName))
        {
            if (!flagIsConversely)
            {
                return factories[typeName];
            }
            return factories[Conversely[typeName]];
        }
        
        throw new ArgumentException("Неправильное название типа операции");
    }
}