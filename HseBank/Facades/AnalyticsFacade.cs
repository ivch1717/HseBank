using HseBank.BaseClasses;
using HseBank.Repository;
using HseBank.TypeOperation;

namespace HseBank.Facades;

public class AnalyticsFacade : IAnalyticsFacade
{
    public IOperationRepository _operationRepository;
    public ICategoryRepository _categoryRepository;

    public AnalyticsFacade(IOperationRepository operationRepository,  ICategoryRepository categoryRepository)
    {
        _operationRepository = operationRepository;
        _categoryRepository = categoryRepository;
    }
    public int DifferenceProfitExpense(DateTime startDate, DateTime endDate, int id)
    {
        int dif = 0;
        foreach (var op in  _operationRepository.GetRep().Values)
        {
            if (op.BankAccountId == id && op.Date >= startDate && op.Date <= endDate)
            {
                dif += op.Type.Name == "Profit" ? op.Amount : -op.Amount;
            }
        }

        return dif;
    }

    public string GroupingByCategory(int id)
    {
        string res = "";
        var expenseGroups = new Dictionary<Category, int>();
        var profitGroups = new Dictionary<Category, int>();
        foreach (var op in _operationRepository.GetRep().Values)
        {
            if (op.BankAccountId != id) continue;
            
            var category = _categoryRepository.GetRep()[op.CategoryId];
            
            if (category.Type.Name == "Expense")
            {
                if (!expenseGroups.ContainsKey(category))
                    expenseGroups[category] = 0;
                expenseGroups[category] += op.Amount;
            }
            else if (category.Type.Name == "Profit")
            {
                if (!profitGroups.ContainsKey(category))
                    profitGroups[category] = 0;
                profitGroups[category] += op.Amount;
            }
        }

        res += "Расходы по категориям\n";
        if (expenseGroups.Count == 0)
        {
            res += "Нет расходов.\n";
        }
        else
        {
            foreach (var pair in expenseGroups)
            {
                res += $"{pair.Key.Name}: {pair.Value}\n";
            }
        }

        res += "\nДоходы по категориям\n";
        if (profitGroups.Count == 0)
        {
            res += "Нет доходов.\n";
        }
        else
        {
            foreach (var pair in profitGroups)
            {
                res += $"{pair.Key.Name}: {pair.Value}\n";
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

        if (best.Count == 0)
        {
            res += "Нет подходящих операций";
        }
        return res;
    }
}