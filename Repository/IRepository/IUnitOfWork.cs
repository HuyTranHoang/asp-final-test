using asp_final_test.Models;
using Type = asp_final_test.Models.Type;

namespace asp_final_test.Repository.IRepository;

public interface IUnitOfWork : IDisposable
{
    GenericRepository<Type> Type { get; }
    GenericRepository<Vaccine> Vaccine { get; }
    GenericRepository<VaccinationSchedule> VaccinationSchedule { get; }
    GenericRepository<VaccinationDate> VaccinationDate { get; }
    int Save();
}