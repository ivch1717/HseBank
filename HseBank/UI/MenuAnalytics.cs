using HseBank.Commands;
using HseBank.Commands.AnalyticsComand;
namespace HseBank.UI;

public class MenuAnalytics
{
    private ICommandResolver _commandResolver;
    private IRequestResolver _requestResolver;
    private IInputOutput _console;
    
    public MenuAnalytics(ICommandResolver commandResolver, IRequestResolver requestResolver, IInputOutput console)
    {
        _commandResolver = commandResolver;
        _requestResolver = requestResolver;
        _console = console;
    }
    
    public string[] Menu2Analytic =
    [
        "Подсчет разницы доходов и расходов за выбранный период",
        "Группировка доходов и расходов по категориям", "топ 5 самых дорогих расходов за периуд"
    ];

    public void RunAnalyticMenu(bool timed)
    {
        switch (_console.ReadingMenu(Menu2Analytic))
        {
            case 0:
                RunDifferenceProfitExpense(timed);
                break;
            case 1:
                RunGroupingByCategory(timed);
                break;
            case 2:
                RunTop5ExpensiveExpense(timed);
                break;
            case 3:
                return;
        }
        
    }
     private void RunDifferenceProfitExpense(bool timed)
    {
        Console.Clear();
        Console.WriteLine("Подсчет разницы доходов и расходов");

        var request = (PeriodRequest)_requestResolver.Resolve(nameof(PeriodRequest));
        request.Start = _console.ReadDate("Введите дату начала (ГГГГ-ММ-ДД или 'ГГГГ-ММ-ДД ЧЧ:ММ'): ");
        request.End = _console.ReadDate("Введите дату конца (ГГГГ-ММ-ДД или 'ГГГГ-ММ-ДД ЧЧ:ММ'): ");
        request.Id = _console.ReadInt("Введите ID аккаунта: ");

        var command = _commandResolver.ResolveWithResult<PeriodRequest>(nameof(DifferenceProfitExpense), timed);
        var result = command.Execute(request);

        Console.WriteLine($"\nРезультат: {result}");
    }

    private void RunGroupingByCategory(bool timed)
    {
        Console.Clear();
        Console.WriteLine("Группировка доходов и расходов по категориям");

        var id = _console.ReadInt("Введите ID аккаунта: ");
        var command = _commandResolver.ResolveWithResult<int>(nameof(GroupingByCategory), timed);
        var result = command.Execute(id);

        Console.WriteLine($"\nРезультат:\n{result}");
    }

    private void RunTop5ExpensiveExpense(bool timed)
    {
        Console.Clear();
        Console.WriteLine("Топ 5 самых дорогих расходов");

        var request = (PeriodRequest)_requestResolver.Resolve(nameof(PeriodRequest));
        request.Start = _console.ReadDate("Введите дату начала (ГГГГ-ММ-ДД или 'ГГГГ-ММ-ДД ЧЧ:ММ'): ");
        request.End = _console.ReadDate("Введите дату конца (ГГГГ-ММ-ДД или 'ГГГГ-ММ-ДД ЧЧ:ММ'): ");
        request.Id = _console.ReadInt("Введите ID аккаунта: ");

        var command = _commandResolver.ResolveWithResult<PeriodRequest>(nameof(Top5ExpensiveExpense), timed);
        var result = command.Execute(request);

        Console.WriteLine($"\nРезультат:\n{result}");
    }
}