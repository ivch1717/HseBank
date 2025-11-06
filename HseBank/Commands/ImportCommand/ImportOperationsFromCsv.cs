using HseBank.Facades;
using HseBank.Import;

namespace HseBank.Commands.ImportCommand;

public class ImportOperationsFromCsv : ICommand<string>
{
    private readonly IOperationFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportOperationsFromCsv(IOperationFacade facade, IImportResolver importResolver)
    {
        _facade = facade;
        _importResolver = importResolver;
    }

    public void Execute(string filepath)
    {
        var rows = _importResolver.GetImporter<string[]>("csv").Import(filepath);
        if (rows == null || rows.Count == 0)
            return;
        
        var headers = rows[0].Select(h => h.Trim()).ToList();
        
        int bankAccIndex = headers.FindIndex(h => h.Equals("BankAccountId", StringComparison.OrdinalIgnoreCase));
        int catIndex     = headers.FindIndex(h => h.Equals("CategoryId", StringComparison.OrdinalIgnoreCase));
        int amountIndex  = headers.FindIndex(h => h.Equals("Amount", StringComparison.OrdinalIgnoreCase));
        int descIndex    = headers.FindIndex(h => h.Equals("Description", StringComparison.OrdinalIgnoreCase));
        
        if (bankAccIndex == -1 || catIndex == -1 || amountIndex == -1)
            return;
        
        for (int i = 1; i < rows.Count; i++)
        {
            try
            {
                var row = rows[i];
                if (row.Length == 0)
                    continue;
            
                if (bankAccIndex >= row.Length || catIndex >= row.Length || amountIndex >= row.Length)
                    continue;

                string bankAccStr = row[bankAccIndex].Trim();
                string catStr = row[catIndex].Trim();
                string amountStr = row[amountIndex].Trim();
                string description = (descIndex != -1 && descIndex < row.Length)
                    ? row[descIndex].Trim()
                    : "";
            
                if (!int.TryParse(bankAccStr, out int bankId))
                    continue;
                if (!int.TryParse(catStr, out int categoryId))
                    continue;
                if (!int.TryParse(amountStr, out int amount))
                    continue;
            
                _facade.AddOperation(bankId, amount, categoryId, description);
            }
            catch (ArgumentException)
            {
               continue;
            }
            
        }
    }
}
