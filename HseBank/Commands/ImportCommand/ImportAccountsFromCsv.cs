using HseBank.Facades;
using HseBank.Import;

namespace HseBank.Commands.ImportCommand;

public class ImportAccountsFromCsv : ICommand<string>
{
    private IBankAccountFacade _facade;
    private IImportResolver _importResolver;

    public ImportAccountsFromCsv(IBankAccountFacade facade, IImportResolver importResolver)
    {
        _facade = facade;
        _importResolver = importResolver;
    }

    public void Execute(string filepath)
    {
        var rows = _importResolver.GetImporter<string[]>("csv").Import(filepath);

        foreach (var row in rows)
        {
            if (row.Length == 0)
                continue;
            
            if (row.Any(col => col.Trim().Equals("Name", StringComparison.OrdinalIgnoreCase)))
                continue;

            try
            {
                string name;
                string balanceStr;
                
                if (row.Length >= 3)
                {
                    name = row[1].Trim();
                    balanceStr = row[2].Trim();
                }
                else if (row.Length == 2)
                {
                    name = row[0].Trim();
                    balanceStr = row[1].Trim();
                }
                else
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                if (!int.TryParse(balanceStr, out int balance))
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