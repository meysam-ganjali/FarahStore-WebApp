namespace FarahStoreApplication.UnitOfWorkPattern;

public interface IUnitOfWork : IDisposable
{
   // ICategoryRepository Category { get; }
    Task Save();
}