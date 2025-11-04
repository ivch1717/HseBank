namespace HseBank.TypeOperation;

public interface IFactoryTypeResolver
{
    public ITypeOperationFactory GetFactory(string typeName, bool flagIsConversely =  false);
}