namespace HseBank.TypeOperation;

public class ProfitFactory : ITypeOperationFactory
{
    public ITypeOperation Create()
    {
        return new Profit();
    }
}