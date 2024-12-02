namespace StajTakipSistemi.Database;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}