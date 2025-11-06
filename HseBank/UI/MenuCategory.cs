using HseBank.Commands;
using HseBank.Commands.CategoryCommand;
using HseBank.Commands.ExportCommand;
using HseBank.Commands.ImportCommand;

namespace HseBank.UI;

public class MenuCategory
{
    private ICommandResolver _commandResolver;
    private IRequestResolver _requestResolver;
    private IInputOutput _console;
    
    public MenuCategory(ICommandResolver commandResolver, IRequestResolver requestResolver, IInputOutput console)
    {
        _commandResolver = commandResolver;
        _requestResolver = requestResolver;
        _console = console;
    }
    
    public string[] Menu2Category = ["Создать категорию", "Удалить категорию", "Изменить имя категории", "Вывести все категории", "экспорт данных в файл", "импорт данных из файлов"];
    
    public void RunCategoryMenu(bool timed)
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
                string fileName = _console.ReadString("Введите название файла с расширением куда импортировать, " +
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
}