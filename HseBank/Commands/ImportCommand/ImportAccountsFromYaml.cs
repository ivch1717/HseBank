using HseBank.Facades;
using HseBank.Import;
namespace HseBank.Commands.ImportCommand;

public class ImportAccountsFromYaml : ICommand<string>
{
    private readonly IBankAccountFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportAccountsFromYaml(IBankAccountFacade facade, IImportResolver importResolver)
    {
        _facade = facade;
        _importResolver = importResolver;
    }

    public void Execute(string filepath)
    {
        var records = _importResolver.GetImporter<Dictionary<string, object>>("yaml").Import(filepath);
        foreach (var record in records)
        {
            try
            {
                string? nameKey = record.Keys.FirstOrDefault(k => k.Equals("Name", StringComparison.OrdinalIgnoreCase));
                string? balanceKey = record.Keys.FirstOrDefault(k => k.Equals("Balance", StringComparison.OrdinalIgnoreCase));

                if (nameKey == null || balanceKey == null)
                    continue;

                string name = record[nameKey]?.ToString()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                if (!int.TryParse(record[balanceKey]?.ToString(), out int balance))
                    continue;

                _facade.AddAccount(name, balance);
            }
            catch (ArgumentException)
            {
                continue;
            }
        }
    }
}