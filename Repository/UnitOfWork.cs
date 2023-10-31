using asp_final_test.Data;
using asp_final_test.Models;
using asp_final_test.Repository.IRepository;
using Type = asp_final_test.Models.Type;

namespace asp_final_test.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private GenericRepository<Type>? _type;
    private GenericRepository<Vaccine>? _vaccine;
    private GenericRepository<VaccinationSchedule>? _vaccinationSchedule;
    private GenericRepository<VaccinationDate>? _vaccinationDate;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public GenericRepository<Type> Type
    {
        get
        {
            if (_type == null)
            {
                _type = new GenericRepository<Type>(_dbContext);
            }

            return _type;
        }
    }

    public GenericRepository<Vaccine> Vaccine
    {
        get
        {
            if (_vaccine == null)
            {
                _vaccine = new GenericRepository<Vaccine>(_dbContext);
            }

            return _vaccine;
        }
    }

    public GenericRepository<VaccinationSchedule> VaccinationSchedule
    {
        get
        {
            if (_vaccinationSchedule == null)
            {
                _vaccinationSchedule = new GenericRepository<VaccinationSchedule>(_dbContext);
            }

            return _vaccinationSchedule;
        }
    }

    public GenericRepository<VaccinationDate> VaccinationDate
    {
        get
        {
            if (_vaccinationDate == null)
            {
                _vaccinationDate = new GenericRepository<VaccinationDate>(_dbContext);
            }

            return _vaccinationDate;
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