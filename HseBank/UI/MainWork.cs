using HseBank.Commands;
using HseBank.Commands.AnalyticsComand;
using HseBank.Commands.BankAccountCommand;
using HseBank.Commands.CategoryCommand;
using HseBank.Commands.OperationCommand;
using HseBank.Commands.ExportCommand;
using HseBank.Commands.ImportCommand;
namespace HseBank.UI;

public class MainWork
{
    public string[] Menu1 = ["Работа с аккаунтами", "Работа с категориями", "Работа с операцми", "аналитика", "выход"];
    public string[] Menu2Account = ["Создать аккаунт", "Удалить аккаунт", "Изменить имя аккаунта", "Вывести все аккаунты", "экспорт данных в файл", "импорт данных из файлов"];
    public string[] Menu2Category = ["Создать категорию", "Удалить категорию", "Изменить имя категории", "Вывести все категории", "экспорт данных в файл", "импорт данных из файлов"];
    public string[] Menu2Operation = ["Создать операцию", "Удалить операцию", "Изменить описание операции", "вывести все операции", "экспорт данных в файл", "импорт данных из файлов"];

    public string[] Menu2Analytic =
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
            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться...");
            Console.ReadKey();
        }
    }
    
    private void RunMenu()
    {
        Console.Clear();
        bool timed = _console.ReadingMenu(["Нет", "Да"], "Включить измерение времени выполнения? (y/n): ") == 1;
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
    
    private void RunAnalyticMenu(bool timed)
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
    
    private void RunAccountMenu(bool timed)
    {
        switch (_console.ReadingMenu(Menu2Account))
        {
            case 0:
                var accReq = (AccountRequest)_requestResolver.Resolve(nameof(AccountRequest));
                accReq.Name = _console.ReadString("Введите имя аккаунта: ");
                accReq.Balance = _console.ReadInt("Введите начальный баланс: ");
                var addAcc = _commandResolver.Resolve<AccountRequest>(nameof(AddAccount), timed);
                addAcc.Execute(accReq);
                Console.WriteLine("Аккаунт успешно создан");
                break;

            case 1:
                var idToRemove = _console.ReadInt("Введите ID аккаунта для удаления: ");
                var removeAcc = _commandResolver.Resolve<int>(nameof(RemoveAccount), timed);
                removeAcc.Execute(idToRemove);
                Console.WriteLine("Аккаунт удалён");
                break;

            case 2:
                var renameReq = (AccountRenameRequest)_requestResolver.Resolve(nameof(AccountRenameRequest));
                renameReq.Id = _console.ReadInt("Введите ID аккаунта: ");
                renameReq.NewName = _console.ReadString("Введите новое имя: ");
                var renameAcc = _commandResolver.Resolve<AccountRenameRequest>(nameof(ChangeAccountName), timed);
                renameAcc.Execute(renameReq);
                Console.WriteLine("Имя аккаунта изменено");
                break;

            case 3:
                var getAllAcc = _commandResolver.ResolveWithResult<object>(nameof(GetAllAccounts), timed);
                Console.WriteLine(getAllAcc.Execute(null));
                break;
            case 4:
                var exportAcc =  _commandResolver.Resolve<string>(nameof(ExportAccount), timed);
                string fileName = _console.ReadString("Введите название файла куда импортировать, " +
                                                      "доступный формат: json, csv, yaml (файл будет сохранён в папку data) : ");
                exportAcc.Execute(fileName);
                Console.WriteLine("Данные успешно экспортированы");
                break;
            case 5:
                RunImportAccount(timed);
                break;
        }
    }

    private void RunImportAccount(bool timed)
    {
        string filepath = _console.ReadString("Введите полный путь к файлу, доступные форматы: " +
                                              "csv, json, yaml \n(некорректные данные будут просто пропускаться, " +
                                              "а так же id будет сами высчитываться в целях безопасности," +
                                              "эти данные могут быть, могут не быть, они никак не влияют) : ");
        if (!filepath.Contains("."))
        {
            Console.WriteLine("неправильное название файла");
            return;
        }
        string expansion = filepath.Substring(filepath.LastIndexOf('.') + 1);
        switch (expansion)
        {
            case "csv":
                var importAccCsv =  _commandResolver.Resolve<string>(nameof(ImportAccountsFromCsv), timed);
                importAccCsv.Execute(filepath);
                break;
            case "json":
                var exportCatJson =  _commandResolver.Resolve<string>(nameof(ImportAccountsFromJson), timed);
                exportCatJson.Execute(filepath);
                break;
            case "yaml":
                var importAccYaml =  _commandResolver.Resolve<string>(nameof(ImportAccountsFromYaml), timed);
                importAccYaml.Execute(filepath);
                break;
        }
        Console.WriteLine("Данные успешно импортированы");
    }
    
    private void RunCategoryMenu(bool timed)
    {
        switch (_console.ReadingMenu(Menu2Category))
        {
            case 0:
                var catReq = (CategoryRequest)_requestResolver.Resolve(nameof(CategoryRequest));
                catReq.Name = _console.ReadString("Введите имя категории: ");
                catReq.TypeName = _console.ReadString("Введите тип (Доход / Расход): ");
                var addCat = _commandResolver.Resolve<CategoryRequest>(nameof(AddCategory), timed);
                addCat.Execute(catReq);
                Console.WriteLine("Категория создана");
                break;

            case 1:
                var idToRemove = _console.ReadInt("Введите ID категории для удаления: ");
                var removeCat = _commandResolver.Resolve<int>(nameof(RemoveCategory), timed);
                removeCat.Execute(idToRemove);
                Console.WriteLine("Категория удалена");
                break;

            case 2:
                var renameReq = (CategoryRenameRequest)_requestResolver.Resolve(nameof(CategoryRenameRequest));
                renameReq.Id = _console.ReadInt("Введите ID категории: ");
                renameReq.NewName = _console.ReadString("Введите новое имя: ");
                var renameCat = _commandResolver.Resolve<CategoryRenameRequest>(nameof(ChangeCategoryName), timed);
                renameCat.Execute(renameReq);
                Console.WriteLine("Имя категории изменено");
                break;

            case 3:
                var getAllCat = _commandResolver.ResolveWithResult<object>(nameof(GetAllCategories), timed);
                Console.WriteLine(getAllCat.Execute(null));
                break;
            case 4:
                var exportCat =  _commandResolver.Resolve<string>(nameof(ExportCategory), timed);
                string fileName = _console.ReadString("Введите название файла куда импортировать, " +
                                                      "доступный формат: json, csv, yaml (файл будет сохранён в папку data) : ");
                exportCat.Execute(fileName);
                Console.WriteLine("Данные успешно экспортированы");
                break;
            case 5:
                RunImportCategory(timed);
                break;
        }
    }
    
    private void RunImportCategory(bool timed)
    {
        string filepath = _console.ReadString("Введите полный путь к файлу, доступные форматы: " +
                                              "csv, json, yaml \n(некорректные данные будут просто пропускаться, " +
                                              "а так же id будет сами высчитываться в целях безопасности," +
                                              "эти данные могут быть, могут не быть, они никак не влияют) : ");
        if (!filepath.Contains("."))
        {
            Console.WriteLine("неправильное название файла");
            return;
        }
        string expansion = filepath.Substring(filepath.LastIndexOf('.') + 1);
        switch (expansion)
        {
            case "csv":
                var importAccCsv =  _commandResolver.Resolve<string>(nameof(ImportCategoriesFromCsv), timed);
                importAccCsv.Execute(filepath);
                break;
            case "json":
                var exportCatJson =  _commandResolver.Resolve<string>(nameof(ImportCategoriesFromJson), timed);
                exportCatJson.Execute(filepath);
                break;
            case "yaml":
                var importAccYaml =  _commandResolver.Resolve<string>(nameof(ImportCategoriesFromYaml), timed);
                importAccYaml.Execute(filepath);
                break;
        }
        Console.WriteLine("Данные успешно импортированы");
    }
    
    private void RunOperationMenu(bool timed)
    {
        switch (_console.ReadingMenu(Menu2Operation))
        {
            case 0:
                var opReq = (OperationRequest)_requestResolver.Resolve(nameof(OperationRequest));
                opReq.BankAccountId = _console.ReadInt("Введите ID аккаунта: ");
                opReq.CategoryId = _console.ReadInt("Введите ID категории: ");
                opReq.Amount = _console.ReadInt("Введите сумму операции: ");
                opReq.Description = _console.ReadString("Введите описание (опционально): ");
                var addOp = _commandResolver.Resolve<OperationRequest>(nameof(AddOperation), timed);
                addOp.Execute(opReq);
                Console.WriteLine("Операция создана");
                break;

            case 1:
                var idToRemove = _console.ReadInt("Введите ID операции для удаления: ");
                var removeOp = _commandResolver.Resolve<int>(nameof(RemoveOperation), timed);
                removeOp.Execute(idToRemove);
                Console.WriteLine("Операция удалена");
                break;

            case 2:
                var descReq =
                    (OperationChangeDescriptionRequest)_requestResolver.Resolve(
                        nameof(OperationChangeDescriptionRequest));
                descReq.Id = _console.ReadInt("Введите ID операции: ");
                descReq.Description = _console.ReadString("Введите новое описание: ");
                var changeDesc =
                    _commandResolver.Resolve<OperationChangeDescriptionRequest>(nameof(ChangeOperationDescription),
                        timed);
                changeDesc.Execute(descReq);
                Console.WriteLine("Описание операции изменено");
                break;

            case 3:
                var getAllOp = _commandResolver.ResolveWithResult<object>(nameof(GetAllOperations), timed);
                Console.WriteLine(getAllOp.Execute(null));
                break;
            case 4:
                var exportOp =  _commandResolver.Resolve<string>(nameof(ExportOperation), timed);
                string fileName = _console.ReadString("Введите название файла куда импортировать, " +
                                                      "доступный формат: json, csv, yaml (файл будет сохранён в папку data) : ");
                exportOp.Execute(fileName);
                Console.WriteLine("Данные успешно экспортированы");
                break;
            case 5:
                RunImportOperation(timed);
                break;
        }
    }
    
    private void RunImportOperation(bool timed)
    {
        string filepath = _console.ReadString("Введите полный путь к файлу, доступные форматы: " +
                                              "csv, json, yaml \n(некорректные данные будут просто пропускаться, " +
                                              "а так же id, дата операции и тип операции будут сами высчитываться в целях безопасности," +
                                              "эти данные могут быть, могут не быть, они никак не влияют) : ");
        if (!filepath.Contains("."))
        {
            Console.WriteLine("неправильное название файла");
            return;
        }
        string expansion = filepath.Substring(filepath.LastIndexOf('.') + 1);
        switch (expansion)
        {
            case "csv":
                var importAccCsv =  _commandResolver.Resolve<string>(nameof(ImportOperationsFromCsv), timed);
                importAccCsv.Execute(filepath);
                break;
            case "json":
                var exportCatJson =  _commandResolver.Resolve<string>(nameof(ImportOperationsFromJson), timed);
                exportCatJson.Execute(filepath);
                break;
            case "yaml":
                var importAccYaml =  _commandResolver.Resolve<string>(nameof(ImportOperationsFromYaml), timed);
                importAccYaml.Execute(filepath);
                break;
        }
        Console.WriteLine("Данные успешно импортированы");
    }
};