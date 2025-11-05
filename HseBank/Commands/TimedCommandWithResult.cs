using System.Diagnostics;
using HseBank.UI;

namespace HseBank.Commands;

public class TimedCommandWithResult<TRequest> : ICommandWithResult<TRequest>
{
    private ICommandWithResult<TRequest> _inner;
    private IInputOutput  _inputOutput;

    public TimedCommandWithResult(ICommandWithResult<TRequest> inner, IInputOutput inputOutput)
    {
        _inner = inner;
        _inputOutput = inputOutput;
    }
    
    public string Execute(TRequest request)
    {
        var sw = Stopwatch.StartNew();
        var result = _inner.Execute(request);
        sw.Stop();
        
        _inputOutput.OutputTimeWotkingCommand(sw);
        return result;
    }
}