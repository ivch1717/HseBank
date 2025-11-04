namespace HseBank.Factories;

public class BankAccountFactory : IBankAccountFactory
{
    public BankAccount Create(int id, string name, int balance)
    {
        if (name == "")
        {
            throw new ArgumentException("Имя аккаунта не может быть пустым");
        }

        if (balance < 0)
        {
            throw new ArgumentException("Баланс не мжет быть отрицательным");
        }
        return new BankAccount(id, name, balance);
    }
}