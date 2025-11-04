using Spectre.Console;
namespace HseBank.UI;

public class MainWork
{
    public string[] Menu1 = ["Работа с аккаунтами", "Работа с категориями", "Работа с операцми", "аналитика"];
    public string[] Menu2Account = ["Создать аккаунт", "Удалить аккаунт", "Изменить имя аккаунта"];
    public string[] Menu2Category = ["Создать категорию", "Удалить категорию", "Изменить имя категории"];
    public string[] Menu2Operation = ["Создать операцию", "Удалить операцию", "Изменить описание операции"];
    
    
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
            switch (ReadingMenu(Menu1))
            {
                case 0:
                    RunAccountMenu();
                    break;
                case 1:
                    RunCategoryMenu();
                    break;
                case 2:
                    RunOperationMenu();
                    break;
                case 3:
                    Console.WriteLine("Выход из программы...");
                    Environment.Exit(0);
                    break;
            }
        }
    }
    
    private void RunAccountMenu()
    {
        while (true)
        {
            switch (ReadingMenu(Menu2Account))
            {
                case 0:
                    var name = ReadString("Введите имя аккаунта: ");
                    var balance = ReadInt("Введите начальный баланс: ");
                    Console.WriteLine($"✅ Аккаунт создан: {name}, баланс {balance}");
                    break;

                case 1:
                    var idToRemove = ReadInt("Введите ID аккаунта для удаления: ");
                    Console.WriteLine($"✅ Аккаунт с id={idToRemove} удалён");
                    break;

                case 2:
                    var idToRename = ReadInt("Введите ID аккаунта: ");
                    var newName = ReadString("Введите новое имя: ");
                    Console.WriteLine($"✅ Имя аккаунта с id={idToRename} изменено на '{newName}'");
                    break;
            }
        }
    }
    
    private void RunCategoryMenu()
    {
        while (true)
        {
            switch (ReadingMenu(Menu2Category))
            {
                case 0:
                    var categoryName = ReadString("Введите имя категории: ");
                    var typeName = ReadString("Введите тип (Profit / Expense): ");
                    Console.WriteLine($"✅ Категория '{categoryName}' с типом {typeName} создана");
                    break;

                case 1:
                    var categoryId = ReadInt("Введите ID категории для удаления: ");
                    Console.WriteLine($"✅ Категория с id={categoryId} удалена");
                    break;

                case 2:
                    var catIdToRename = ReadInt("Введите ID категории: ");
                    var catNewName = ReadString("Введите новое имя: ");
                    Console.WriteLine($"✅ Имя категории с id={catIdToRename} изменено на '{catNewName}'");
                    break;
            }
        }
    }
    
    private void RunOperationMenu()
    {
        while (true)
        {
            switch (ReadingMenu(Menu2Operation))
            {
                case 0:
                    var accountId = ReadInt("Введите ID аккаунта: ");
                    var categoryId = ReadInt("Введите ID категории: ");
                    var amount = ReadInt("Введите сумму операции: ");
                    var desc = ReadString("Введите описание (опционально): ");
                    Console.WriteLine($"✅ Операция создана: счёт={accountId}, категория={categoryId}, сумма={amount}, описание='{desc}'");
                    break;

                case 1:
                    var operationId = ReadInt("Введите ID операции для удаления: ");
                    Console.WriteLine($"✅ Операция с id={operationId} удалена");
                    break;

                case 2:
                    var opId = ReadInt("Введите ID операции: ");
                    var newDesc = ReadString("Введите новое описание: ");
                    Console.WriteLine($"✅ Описание операции с id={opId} изменено на '{newDesc}'");
                    break;
            }
        }
    }
    
    private int ReadingMenu(string[] data)
    {
        Console.Clear();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите пункт меню:")
                .PageSize(10)
                .MoreChoicesText("[grey](Используйте стрелки для навигации)[/]")
                .AddChoices(data)
        );

        return Array.IndexOf(data, selection);
    }

    private int ReadInt(string mes = "")
    {
        Console.WriteLine(mes);
        int result;
        while (!int.TryParse(mes, out result))
        {
            Console.WriteLine("Ошибка, введите целое число");
        }

        return result;
    }
    
    private string ReadString(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? string.Empty;
    }
}