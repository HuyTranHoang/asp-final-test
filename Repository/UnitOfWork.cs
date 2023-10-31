using asp_final_test.Data;
using asp_final_test.Models;
using asp_final_test.Repository.IRepository;
namespace asp_final_test.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private GenericRepository<Category>? _category;
    private GenericRepository<Vaccine>? _product;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public GenericRepository<Category> Category
    {
        get
        {
            if (_category == null)
            {
                _category = new GenericRepository<Category>(_dbContext);
            }

            return _category;
        }
    }

    public GenericRepository<Vaccine> Vaccine
    {
        get
        {
            if (_product == null)
            {
                _product = new GenericRepository<Vaccine>(_dbContext);
            }

            return _product;
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public int Save()
    {
        return _dbContext.SaveChanges();
    }
}