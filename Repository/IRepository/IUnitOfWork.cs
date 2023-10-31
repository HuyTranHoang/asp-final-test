using asp_final_test.Models;

namespace asp_final_test.Repository.IRepository;

public interface IUnitOfWork : IDisposable
{
    GenericRepository<Category> Category { get; }
    GenericRepository<Vaccine> Vaccine { get; }
    int Save();
}