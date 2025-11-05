using System.Diagnostics;

namespace HseBank.UI;

public interface IInputOutput
{
    public DateTime ReadDate(string message = "");
    public string ReadString(string message = "");
    public int ReadInt(string mes = "");
    public int ReadingMenu(string[] data);

    public void OutputTimeWotkingCommand(Stopwatch sw);
}