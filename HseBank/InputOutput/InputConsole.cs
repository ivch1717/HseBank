namespace HseBank.InputOutput;

public class InputConsole
{
    public string Input(string message)
    {
        if (message != "")
        {
            Console.WriteLine(message);
        }

        string s = Console.ReadLine();
        return s;   
    }

    public int InputInt(string message)
    {
        string s = Input(message);
        int x = 0;
        while (!int.TryParse(s, out x))
        {
            Console.WriteLine("Ошибка, введите целое число");
            s = Input(message);
        }
        return x;
    }
}