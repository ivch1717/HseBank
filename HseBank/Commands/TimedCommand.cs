using HseBank.UI;
using System.Diagnostics;

namespace HseBank.Commands;

public class TimedCommand<TRequest> : ICommand<TRequest>
{
    private readonly ICommand<TRequest> _inner;
    private IInputOutput  _inputOutput;

    public TimedCommand(ICommand<TRequest> inner,  IInputOutput inputOutput)
    {
        _inner = inner;
        _inputOutput = inputOutput;
    }

    public void Execute(TRequest request)
    {
        var sw = Stopwatch.StartNew();
        _inner.Execute(request);
        sw.Stop();

        _inputOutput.OutputTimeWotkingCommand(sw);
    }
}