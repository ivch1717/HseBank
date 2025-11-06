using HseBank.Commands;
namespace HseBank.UI;

public class MainWork
{
    public string[] Menu1 = ["Работа с аккаунтами", "Работа с категориями", "Работа с операцми", "аналитика", "выход"];
    private IInputOutput _console;
    private MenuAccount _menuAccount;
    public MenuAnalytics _menuAnalytics;
    public MenuCategory _menuCategory;
    public MenuOperation _menuOperation;

    public MainWork(IInputOutput console, MenuAccount menuAccount,  MenuAnalytics menuAnalytics, MenuCategory menuCategory, MenuOperation menuOperation)
    {
        _console = console;
        _menuAccount = menuAccount;
        _menuAnalytics = menuAnalytics;
        _menuCategory = menuCategory;
        _menuOperation = menuOperation;
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
                _menuAccount.RunAccountMenu(timed);
                break;
            case 1:
                _menuCategory.RunCategoryMenu(timed);
                break;
            case 2:
                _menuOperation.RunOperationMenu(timed);
                break;
            case 3:
                _menuAnalytics.RunAnalyticMenu(timed);
                break;
            case 4:
                Console.WriteLine("Выход из программы...");
                Environment.Exit(0);
                break;
        }
    }
    
};