using HseBank.Commands;
using HseBank.Commands.BankAccountCommand;
using HseBank.Commands.ExportCommand;
using HseBank.Commands.ImportCommand;
namespace HseBank.UI;

public class MenuAccount
{
    private ICommandResolver _commandResolver;
    private IRequestResolver _requestResolver;
    private IInputOutput _console;

    public MenuAccount(ICommandResolver commandResolver, IRequestResolver requestResolver, IInputOutput console)
    {
        _commandResolver = commandResolver;
        _requestResolver = requestResolver;
        _console = console;
    }
    
    public string[] Menu2Account = ["Создать аккаунт", "Удалить аккаунт", "Изменить имя аккаунта", "Вывести все аккаунты", "экспорт данных в файл", "импорт данных из файлов"];
    public void RunAccountMenu(bool timed)
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
                string fileName = _console.ReadString("Введите название файла с расширением куда импортировать, " +
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
}