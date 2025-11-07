using HseBank.Commands;
using HseBank.Commands.OperationCommand;
using HseBank.Commands.ExportCommand;
using HseBank.Commands.ImportCommand;

namespace HseBank.UI;

public class MenuOperation
{
    private ICommandResolver _commandResolver;
    private IRequestResolver _requestResolver;
    private IInputOutput _console;
    
    public MenuOperation(ICommandResolver commandResolver, IRequestResolver requestResolver, IInputOutput console)
    {
        _commandResolver = commandResolver;
        _requestResolver = requestResolver;
        _console = console;
    }
    
    public string[] Menu2Operation = ["Создать операцию", "Удалить операцию", "Изменить описание операции", "вывести все операции", "экспорт данных в файл", "импорт данных из файлов"];
    
    public void RunOperationMenu(bool timed)
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
                string fileName = _console.ReadString("Введите название файла с расширением куда импортировать, " +
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
                                              "а так же id и тип операции будут сами высчитываться в целях безопасности," +
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
}