namespace IoCReflect
{
    public interface ILogger
    {
        
    }

    public class SqlServerLogger : ILogger
    {
        
    }

    public interface IRepository<T>
    {
        
    }

    public class SqlRepository<T> : IRepository<T>
    {
        public SqlRepository(ILogger logger)
        {
            
        }
    }

    public class ProductionService
    {
        public ProductionService(IRepository<Customer> repository, ILogger logger )
        {
            
        }
    }
}
