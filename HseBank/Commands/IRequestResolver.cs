namespace HseBank.Commands;

public interface IRequestResolver
{
    object Resolve(string name);
}