namespace HseBank.InputOutput;

public interface IInputService
{
    public string Input(string message = "");
    public int InputInt(string message = "");
    public void Wait(string message = "");
}