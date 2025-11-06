using Microsoft.Extensions.DependencyInjection;
using HseBank.Commands;
using HseBank.Commands.AnalyticsComand;
using HseBank.Facades;
using HseBank.Factories;
using HseBank.Repository;
using HseBank.service;
using HseBank.TypeOperation;
using HseBank.Commands.BankAccountCommand;
using HseBank.Commands.CategoryCommand;
using HseBank.Commands.OperationCommand;
using HseBank.Commands.ExportCommand;
using HseBank.Commands.ImportCommand;
using HseBank.UI;
using HseBank.Export;
using HseBank.Import;

namespace  HseBank
{
    class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            
            services.AddSingleton<ICommandResolver,  CommandResolver>();
            services.AddSingleton<IRequestResolver, RequestResolver>();
            services.AddTransient<DifferenceProfitExpense>();
            services.AddTransient<GroupingByCategory>();
            services.AddTransient<Top5ExpensiveExpense>();
            
            services.AddTransient<AddAccount>();
            services.AddTransient<RemoveAccount>();
            services.AddTransient<ChangeAccountName>();
            services.AddTransient<GetAllAccounts>();
            
            services.AddTransient<AddCategory>();
            services.AddTransient<RemoveCategory>();
            services.AddTransient<ChangeCategoryName>();
            services.AddTransient<GetAllCategories>();
            
            services.AddTransient<AddOperation>();
            services.AddTransient<RemoveOperation>();
            services.AddTransient<ChangeOperationDescription>();
            services.AddTransient<GetAllOperations>();
            
            services.AddTransient<ExportAccount>();
            services.AddTransient<ExportCategory>();
            services.AddTransient<ExportOperation>();
            
            services.AddTransient<ImportAccountsFromCsv>();
            services.AddTransient<ImportAccountsFromJson>();
            services.AddTransient<ImportAccountsFromYaml>();
            
            services.AddTransient<ImportOperationsFromCsv>();
            services.AddTransient<ImportOperationsFromJson>();
            services.AddTransient<ImportOperationsFromYaml>();

            services.AddTransient<ImportCategoriesFromCsv>();
            services.AddTransient<ImportCategoriesFromJson>();
            services.AddTransient<ImportCategoriesFromYaml>();
            
            services.AddSingleton<IBankAccountFacade, BankAccountFacade>();
            services.AddSingleton<ICategoryFacade, CategoryFacade>();
            services.AddSingleton<IOperationFacade, OperationFacade>();
            services.AddSingleton<IAnalyticsFacade, AnalyticsFacade>();
            
            services.AddSingleton<IBankAccountFactory, BankAccountFactory>();
            services.AddSingleton<ICategoryFactory, CategoryFactory>();
            services.AddSingleton<IOperationFactory, OperationFactory>();
            
            services.AddSingleton<IBankAccountRepository,  BankAccountRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IOperationRepository, OperationRepository>();
            
            services.AddSingleton<IFactoryTypeResolver, FactoryTypeResolver>();
            
            services.AddSingleton<IExportResolver, ExportResolver>();
            services.AddSingleton<IImportResolver, ImportResolver>();

            services.AddSingleton<IInputOutput, ConsoleWork>();
            services.AddSingleton<MainWork>();

            services.AddSingleton<MenuAccount>();
            services.AddSingleton<MenuCategory>();
            services.AddSingleton<MenuOperation>();
            services.AddSingleton<MenuAnalytics>();
            var serviceProvider = services.BuildServiceProvider();
            
            var app = serviceProvider.GetRequiredService<MainWork>();
            app.Run();
        }
    }
}

