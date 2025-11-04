using HseBank.BaseClasses;
using HseBank.Repository;
using HseBank.TypeOperation;

namespace HseBank.Facades;

public class AnalyticsFacade : IAnalyticsFacade
{
    public IOperationRepository _operationRepository;

    public AnalyticsFacade(IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }
    public int DifferenceProfitExpense(DateTime startDate, DateTime endDate, int id)
    {
        int dif = 0;
        foreach (var op in  _operationRepository.GetRep().Values)
        {
            if (op.BankAccountId == id && op.Date >= startDate && op.Date <= endDate)
            {
                dif = op.Type.Count(dif, op.Amount);
            }
        }

        return dif;
    }

    public string GroupingByCategory(int id)
    {
        string res = "";
        Dictionary<ITypeOperation, int>  operations = new Dictionary<ITypeOperation, int>();
        foreach (var op in _operationRepository.GetRep().Values)
        {
            operations[op.Type] += op.Amount;
        }
        res += "Expense:\n";
        foreach (var op in operations)
        {
            if (op.Key.Name == "Expense")
            {
                res += $"{op.Key.Name} : {op.Value}\n";
            }
        }
        res += "Profit:\n";
        foreach (var op in operations)
        {
            if (op.Key.Name == "Profit")
            {
                res += $"{op.Key.Name} : {op.Value}\n";
            }
        }
        return res;
    }

    public string Top5ExpensiveExpense(DateTime startDate, DateTime endDate, int id)
    {
        Dictionary<int, Operation> best = new Dictionary<int, Operation>();
        foreach (var op in  _operationRepository.GetRep().Values)
        {
            if (op.BankAccountId == id && op.Type.Name == "Expense" && op.Date >= startDate && op.Date <= endDate)
            {
                best.Add(op.Amount, op);
            }
        }

        best = best.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        string res = "";
        for (int i = 0; i < Math.Min(5, best.Count); i++)
        {
            res += best.ElementAt(i).Value.ToString() + '\n';
        }
        return res;
    }
}