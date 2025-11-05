using HseBank.Commands;
using HseBank.Commands.AnalyticsComand;
namespace HseBank.UI;

public class MainWork
{
    public string[] Menu1 = ["Работа с аккаунтами", "Работа с категориями", "Работа с операцми", "аналитика", "выход"];
    public string[] Menu2Account = ["Создать аккаунт", "Удалить аккаунт", "Изменить имя аккаунта"];
    public string[] Menu2Category = ["Создать категорию", "Удалить категорию", "Изменить имя категории"];
    public string[] Menu2Operation = ["Создать операцию", "Удалить операцию", "Изменить описание операции"];

    public string[] MenuAnalytic =
    [
        "Подсчет разницы доходов и расходов за выбранный период",
        "Группировка доходов и расходов по категориям", "топ 5 самых дорогих расходов за периуд"
    ];
    
    private ICommandResolver _commandResolver;
    private IRequestResolver _requestResolver;
    private IInputOutput _console;

    public MainWork(ICommandResolver commandResolver, IRequestResolver requestResolver, IInputOutput console)
    {
        _commandResolver = commandResolver;
        _requestResolver = requestResolver;
        _console = console;
    }
    
    
    public void Run()
    {
        ExecuteWithErrorHandling(RunMenu);
    }

    private void ExecuteWithErrorHandling(Action action)
    {
        while (true)
        {
            try
            {
                action();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Ошибка: значение не может быть null");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка ввода: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка формата данных: {ex.Message}");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"Ошибка преобразования типа:");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка индекса: неверный индекс массива или коллекции");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Ошибка ключа: элемент не найден в словаре");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Ошибка ссылки: объект не инициализирован");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка операции");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Ошибка: операция не поддерживается");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Ошибка переполнения: число слишком большое или маленькое");
            }
        }
    }
    
    private void RunMenu()
    {
        while (true)
        {
            bool timed = _console.ReadString("Включить измерение времени выполнения? (y/n): ").Trim().ToLower() == "y";
            switch (_console.ReadingMenu(Menu1))
            {
                case 0:
                    RunAccountMenu(timed);
                    break;
                case 1:
                    RunCategoryMenu(timed);
                    break;
                case 2:
                    RunOperationMenu(timed);
                    break;
                case 3:
                    RunAnalyticMenu(timed);
                    break;
                case 4:
                    Console.WriteLine("Выход из программы...");
                    Environment.Exit(0);
                    break;
            }
        }
    }
    
    private void RunAnalyticMenu(bool timed)
    {
        switch (_console.ReadingMenu(MenuAnalytic))
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

        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться...");
        Console.ReadKey();
    }
    
    private void RunDifferenceProfitExpense(bool timed)
    {
        Console.Clear();
        Console.WriteLine("=== Подсчет разницы доходов и расходов ===");

        var request = (PeriodRequest)_requestResolver.Resolve(nameof(PeriodRequest));
        request.Start = _console.ReadDate("Введите дату начала периода (ГГГГ-ММ-ДД): ");
        request.End = _console.ReadDate("Введите дату конца периода (ГГГГ-ММ-ДД): ");
        request.Id = _console.ReadInt("Введите ID аккаунта: ");

        var command = _commandResolver.ResolveWithResult<PeriodRequest>(nameof(DifferenceProfitExpense), timed);
        var result = command.Execute(request);

        Console.WriteLine($"\nРезультат: {result}");
    }

    private void RunGroupingByCategory(bool timed)
    {
        Console.Clear();
        Console.WriteLine("=== Группировка доходов и расходов по категориям ===");

        var id = _console.ReadInt("Введите ID аккаунта: ");
        var command = _commandResolver.ResolveWithResult<int>(nameof(GroupingByCategory), timed);
        var result = command.Execute(id);

        Console.WriteLine($"\nРезультат:\n{result}");
    }

    private void RunTop5ExpensiveExpense(bool timed)
    {
        
        Console.Clear();
        Console.WriteLine("=== Топ 5 самых дорогих расходов ===");

        var request = (PeriodRequest)_requestResolver.Resolve(nameof(PeriodRequest));
        request.Start = _console.ReadDate("Введите дату начала периода (ГГГГ-ММ-ДД): ");
        request.End = _console.ReadDate("Введите дату конца периода (ГГГГ-ММ-ДД): ");
        request.Id = _console.ReadInt("Введите ID аккаунта: ");

        var command = _commandResolver.ResolveWithResult<PeriodRequest>(nameof(Top5ExpensiveExpense), timed);
        var result = command.Execute(request);

        Console.WriteLine($"\nРезультат:\n{result}");
    }
    
    private void RunAccountMenu(bool timed)
    {
        while (true)
        {
            switch (_console.ReadingMenu(Menu2Account))
            {
                case 0:
                    var name = _console.ReadString("Введите имя аккаунта: ");
                    var balance = _console.ReadInt("Введите начальный баланс: ");
                    Console.WriteLine($"✅ Аккаунт создан: {name}, баланс {balance}");
                    break;

                case 1:
                    var idToRemove = _console.ReadInt("Введите ID аккаунта для удаления: ");
                    Console.WriteLine($"✅ Аккаунт с id={idToRemove} удалён");
                    break;

                case 2:
                    var idToRename = _console.ReadInt("Введите ID аккаунта: ");
                    var newName = _console.ReadString("Введите новое имя: ");
                    Console.WriteLine($"✅ Имя аккаунта с id={idToRename} изменено на '{newName}'");
                    break;
            }
        }
    }
    
    private void RunCategoryMenu(bool timed)
    {
        while (true)
        {
            switch (_console.ReadingMenu(Menu2Category))
            {
                case 0:
                    var categoryName = _console.ReadString("Введите имя категории: ");
                    var typeName = _console.ReadString("Введите тип (Profit / Expense): ");
                    Console.WriteLine($"✅ Категория '{categoryName}' с типом {typeName} создана");
                    break;

                case 1:
                    var categoryId = _console.ReadInt("Введите ID категории для удаления: ");
                    Console.WriteLine($"✅ Категория с id={categoryId} удалена");
                    break;

                case 2:
                    var catIdToRename = _console.ReadInt("Введите ID категории: ");
                    var catNewName = _console.ReadString("Введите новое имя: ");
                    Console.WriteLine($"✅ Имя категории с id={catIdToRename} изменено на '{catNewName}'");
                    break;
            }
        }
    }
    
    private void RunOperationMenu(bool timed)
    {
        while (true)
        {
            switch (_console.ReadingMenu(Menu2Operation))
            {
                case 0:
                    var accountId = _console.ReadInt("Введите ID аккаунта: ");
                    var categoryId = _console.ReadInt("Введите ID категории: ");
                    var amount = _console.ReadInt("Введите сумму операции: ");
                    var desc = _console.ReadString("Введите описание (опционально): ");
                    Console.WriteLine($"✅ Операция создана: счёт={accountId}, категория={categoryId}, сумма={amount}, описание='{desc}'");
                    break;

                case 1:
                    var operationId = _console.ReadInt("Введите ID операции для удаления: ");
                    Console.WriteLine($"✅ Операция с id={operationId} удалена");
                    break;

                case 2:
                    var opId = _console.ReadInt("Введите ID операции: ");
                    var newDesc = _console.ReadString("Введите новое описание: ");
                    Console.WriteLine($"✅ Описание операции с id={opId} изменено на '{newDesc}'");
                    break;
            }
        }
    }
};