namespace HseBank.TypeOperation;

public class ExpenseFactory : ITypeOperationFactory
{
    public ITypeOperation Create()
    {
        return new Expense();
    }
}