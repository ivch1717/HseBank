using System.Diagnostics;
using Spectre.Console;
namespace HseBank.UI;

public class ConsoleWork : IInputOutput
{
    public int ReadingMenu(string[] data)
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

    public int ReadInt(string mes = "")
    {
        Console.WriteLine(mes);
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Ошибка, введите целое число");
        }
        
        return result;
    }
    
    public string ReadString(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? string.Empty;
    }
    
    public DateTime ReadDate(string message)
    {
        Console.Write(message);
        DateTime result;

        while (true)
        {
            var input = Console.ReadLine();

            if (DateTime.TryParse(input, out result))
                return result;

            Console.WriteLine("❌ Ошибка! Введите дату в формате 'ГГГГ-ММ-ДД' или 'ГГГГ-ММ-ДД ЧЧ:ММ':");
            Console.Write(message);
        }
    }

    public void OutputTimeWotkingCommand(Stopwatch sw)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Время выполнения команды : {sw.ElapsedMilliseconds} мс");
        Console.ResetColor();
    }
}